/**
 * Looked Cards Functionality
 * Handles marking items as looked/viewed
 */

(function () {
    'use strict';

    const LookedCards = {
        init() {
            this.attachEventListeners();
        },

        attachEventListeners() {
            const cards = document.querySelectorAll('.looked-card-item');

            cards.forEach(card => {
                // Mark as looked on item click
                card.addEventListener('click', (e) => {
                    // Don't trigger on link click
                    if (e.target.closest('.looked-card-item__link')) {
                        return;
                    }

                    e.preventDefault();
                    this.markAsLooked(card);
                });

                // Optional: Mark as looked when hovering
                card.addEventListener('mouseenter', () => {
                    this.markAsLooked(card);
                });
            });
        },

        markAsLooked(cardElement) {
            const itemId = cardElement.dataset.itemId;
            const lookedCardId = cardElement.dataset.lookedCardId;

            // Prevent marking the same item multiple times
            if (cardElement.classList.contains('marking') || cardElement.classList.contains('is-viewed')) {
                return;
            }

            cardElement.classList.add('marking');

            // Send AJAX request
            fetch(`/api/profile/lookedcard/${itemId}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'X-CSRF-TOKEN': this.getCsrfToken()
                },
                body: JSON.stringify({ itemId: itemId })
            })
                .then(response => {
                    if (!response.ok) {
                        throw new Error(`HTTP error! status: ${response.status}`);
                    }
                    return response.json();
                })
                .then(data => {
                    if (data.success) {
                        // Visual feedback
                        cardElement.classList.add('is-viewed');

                        // Add viewed badge if not already present
                        if (!cardElement.querySelector('.looked-badge')) {
                            this.addViewedBadge(cardElement);
                        }

                        // Update view count
                        this.updateViewCount(cardElement);

                        // Optional: Show toast notification
                        this.showNotification('Товар додано до переглянутих');
                    }
                })
                .catch(error => {
                    console.error('Error marking item as looked:', error);
                })
                .finally(() => {
                    cardElement.classList.remove('marking');
                });
        },

        addViewedBadge(cardElement) {
            const imageContainer = cardElement.querySelector('.looked-card-item__image');
            if (imageContainer && !imageContainer.querySelector('.looked-badge')) {
                const badge = document.createElement('span');
                badge.className = 'looked-badge';
                badge.innerHTML = '<i class="icon-eye"></i> Переглянуто';
                imageContainer.appendChild(badge);
            }
        },

        updateViewCount(cardElement) {
            const viewsElement = cardElement.querySelector('.looked-card-item__views');
            if (viewsElement) {
                const currentText = viewsElement.textContent;
                const matches = currentText.match(/\d+/);
                if (matches) {
                    let count = parseInt(matches[0]) + 1;
                    viewsElement.textContent = `${count} переглядів`;
                }
            }
        },

        getCsrfToken() {
            const token = document.querySelector('input[name="__RequestVerificationToken"]')?.value;
            return token || '';
        },

        showNotification(message, type = 'success') {
            // Create notification element
            const notification = document.createElement('div');
            notification.className = `notification notification--${type}`;
            notification.textContent = message;
            notification.style.cssText = `
                position: fixed;
                top: 20px;
                right: 20px;
                background: ${type === 'success' ? '#34d399' : '#f87171'};
                color: white;
                padding: 12px 20px;
                border-radius: 6px;
                font-weight: 600;
                z-index: 10000;
                animation: slideInRight 0.3s ease;
                box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
            `;

            document.body.appendChild(notification);

            // Auto remove after 3 seconds
            setTimeout(() => {
                notification.style.animation = 'slideOutRight 0.3s ease';
                setTimeout(() => {
                    notification.remove();
                }, 300);
            }, 3000);
        }
    };

    // Add CSS animations
    const style = document.createElement('style');
    style.textContent = `
        @keyframes slideInRight {
            from {
                transform: translateX(400px);
                opacity: 0;
            }
            to {
                transform: translateX(0);
                opacity: 1;
            }
        }

        @keyframes slideOutRight {
            from {
                transform: translateX(0);
                opacity: 1;
            }
            to {
                transform: translateX(400px);
                opacity: 0;
            }
        }
    `;
    document.head.appendChild(style);

    // Initialize when DOM is ready
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', () => {
            LookedCards.init();
        });
    } else {
        LookedCards.init();
    }

    // Expose to window for debugging
    window.LookedCards = LookedCards;
})();
