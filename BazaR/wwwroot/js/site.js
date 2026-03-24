document.addEventListener('DOMContentLoaded', function () {
    const body = document.body;
    const loginModal = document.getElementById('loginModal');
    const registerModal = document.getElementById('registerModal');
    const isAuthenticated = (body.dataset.isAuthenticated || '').toLowerCase() === 'true';

    function getModal(name) {
        if (name === 'login') return loginModal;
        if (name === 'register') return registerModal;
        return null;
    }

    function closeModal(modal) {
        if (!modal) return;

        modal.classList.remove('is-open');
        modal.setAttribute('aria-hidden', 'true');
        body.classList.remove('auth-modal-open');
    }

    function closeAllModals() {
        closeModal(loginModal);
        closeModal(registerModal);
    }

    function openModal(name) {
        if (isAuthenticated) return;

        const modal = getModal(name);
        if (!modal) return;

        document.dispatchEvent(new CustomEvent('sidebar:close'));

        closeAllModals();
        modal.classList.add('is-open');
        modal.setAttribute('aria-hidden', 'false');
        body.classList.add('auth-modal-open');
    }

    document.querySelectorAll('[data-auth-open]').forEach(function (button) {
        button.addEventListener('click', function () {
            openModal(button.dataset.authOpen);
        });
    });

    document.querySelectorAll('[data-auth-switch]').forEach(function (button) {
        button.addEventListener('click', function () {
            openModal(button.dataset.authSwitch);
        });
    });

    document.querySelectorAll('.auth-pass-toggle, .auth-pass-toggle-noterror, .auth-pass-toggle-noterror-login, .auth-pass-toggle-login').forEach(function (button) {

        const eyeOpen = `
        <svg width="21" height="20" viewBox="0 0 21 20" fill="none" xmlns="http://www.w3.org/2000/svg">
	        <path d="M10.4011 4.80071C5.60019 4.80071 1.37647 8.00831 0.365069 12.4783C0.329869 12.6343 0.363269 12.7967 0.457869 12.9299C0.552469 13.0632 0.700469 13.1562 0.869469 13.1887C1.03847 13.2212 1.21447 13.1904 1.35877 13.103C1.50307 13.0157 1.60387 12.8791 1.63907 12.7231C2.52567 8.79311 6.23197 6.00071 10.4011 6.00071C14.5702 6.00071 18.2765 8.79311 19.1631 12.7231C19.1983 12.8791 19.2991 13.0157 19.4434 13.103C19.5878 13.1904 19.7638 13.2212 19.9327 13.1887C20.1017 13.1562 20.2497 13.0632 20.3443 12.9299C20.4389 12.7967 20.4723 12.6343 20.4371 12.4783C19.4257 8.00831 15.202 4.80071 10.4011 4.80071Z" fill="#2D2323"/>
	        <path d="M10.4011 8.40071C8.41277 8.40071 6.80107 9.89831 6.80107 11.7407C6.80107 13.5831 8.41277 15.0807 10.4011 15.0807C12.3894 15.0807 14.0011 13.5831 14.0011 11.7407C14.0011 9.89831 12.3894 8.40071 10.4011 8.40071ZM10.4011 13.8807C9.13277 13.8807 8.10107 12.9231 8.10107 11.7407C8.10107 10.5583 9.13277 9.60071 10.4011 9.60071C11.6694 9.60071 12.7011 10.5583 12.7011 11.7407C12.7011 12.9231 11.6694 13.8807 10.4011 13.8807Z" fill="#2D2323"/>
        </svg>
        `;

        const eyeClosed = `
        <svg width="21" height="20" viewBox="0 0 21 20" fill="none" xmlns="http://www.w3.org/2000/svg">
            <path d="M1.11102 0.175958C1.05059 0.120172 0.978841 0.0759209 0.899879 0.04573C0.820918 0.0155391 0.736288 5.87796e-10 0.650821 0C0.565354 -5.87796e-10 0.480724 0.0155391 0.401763 0.04573C0.322801 0.0759209 0.251055 0.120172 0.190621 0.175958C0.130187 0.231743 0.0822477 0.297971 0.0495409 0.370858C0.016834 0.443745 -6.36779e-10 0.521865 0 0.600758C6.36779e-10 0.679651 0.016834 0.757771 0.0495409 0.830658C0.0822477 0.903545 0.130187 0.969773 0.190621 1.02556L4.74062 5.22316C2.48716 6.64196 0.918371 8.81284 0.364821 11.2784C0.347408 11.3556 0.346641 11.4352 0.362566 11.5127C0.37849 11.5902 0.410794 11.664 0.457632 11.73C0.504469 11.796 0.564924 11.8528 0.635545 11.8972C0.706166 11.9416 0.785569 11.9727 0.869221 11.9888C0.952873 12.0048 1.03914 12.0055 1.12308 11.9908C1.20703 11.9761 1.28702 11.9463 1.35849 11.9031C1.42995 11.8599 1.49149 11.804 1.53959 11.7389C1.58769 11.6737 1.62141 11.6004 1.63882 11.5232C1.88682 10.415 2.37178 9.36372 3.06533 8.43073C3.75887 7.49774 4.64707 6.70182 5.67792 6.08956L7.73972 7.99276C7.20512 8.34859 6.76065 8.80739 6.43706 9.3374C6.11347 9.86742 5.91849 10.456 5.8656 11.0624C5.81271 11.6689 5.90318 12.2786 6.13075 12.8496C6.35831 13.4206 6.71755 13.9391 7.18359 14.3693C7.64964 14.7995 8.21137 15.1311 8.82992 15.3412C9.44848 15.5512 10.1091 15.6347 10.766 15.5859C11.423 15.5371 12.0606 15.3571 12.6348 15.0584C13.209 14.7597 13.706 14.3494 14.0915 13.856L19.6906 19.0256C19.8127 19.1382 19.9782 19.2015 20.1508 19.2015C20.3234 19.2015 20.489 19.1382 20.611 19.0256C20.7331 18.9129 20.8016 18.7601 20.8016 18.6008C20.8016 18.4414 20.7331 18.2886 20.611 18.176L1.11102 0.175958ZM13.1555 12.992C12.8961 13.3741 12.5466 13.6973 12.1332 13.9376C11.7197 14.1778 11.2529 14.3289 10.7674 14.3797C10.282 14.4304 9.79036 14.3795 9.32917 14.2307C8.86799 14.0818 8.44908 13.8389 8.10362 13.5201C7.75816 13.2012 7.49502 12.8145 7.33379 12.3888C7.17256 11.9631 7.11738 11.5093 7.17236 11.0612C7.22733 10.613 7.39105 10.1821 7.65133 9.80049C7.91161 9.41884 8.26175 9.09626 8.67572 8.85676L13.1555 12.9932V12.992Z" fill="#2D2323" />
            <path d="M10.5605 7.20312L14.948 11.2519C14.9073 10.1901 14.4321 9.18195 13.6182 8.43065C12.8043 7.67934 11.7122 7.24071 10.5618 7.20312H10.5605Z" fill="#2D2323" />
            <path d="M10.4011 4.80071C9.66011 4.80071 8.93341 4.88951 8.23531 5.05631L7.19141 4.09271C8.22478 3.76713 9.3092 3.6009 10.4011 3.60071C15.202 3.60071 19.4257 6.80831 20.4371 11.2783C20.4723 11.4343 20.4389 11.5967 20.3443 11.7299C20.2497 11.8632 20.1017 11.9562 19.9327 11.9887C19.7638 12.0212 19.5878 11.9904 19.4434 11.903C19.2991 11.8157 19.1983 11.6791 19.1631 11.5231C18.2765 7.59311 14.5702 4.80071 10.4011 4.80071Z" fill="#2D2323" />
        </svg>`;

        button.addEventListener('click', function () {
            const wrap = button.closest('.auth-input-wrap');
            const input = wrap ? wrap.querySelector('input[type="password"], input[type="text"]') : null;

            if (!input) return;

            if (input.type === 'password') {
                input.type = 'text';
                button.innerHTML = eyeOpen;
            } else {
                input.type = 'password';
                button.innerHTML = eyeClosed;
            }
        });
    });

    if (!isAuthenticated) {
        const openAuth = (body.dataset.openAuth || '').toLowerCase().trim();
        if (openAuth === 'login' || openAuth === 'register') {
            openModal(openAuth);
        }
    } else {
        closeAllModals();
    }
});


// ══════════════════════════════════════════
//   SIDEBAR DRAWER  —  BazaR
// ══════════════════════════════════════════

(function () {
    'use strict';

    const drawer = document.getElementById('sidebarDrawer');
    const overlay = document.getElementById('sidebarOverlay');
    const closeBtn = document.getElementById('sidebarClose');

    if (!drawer) return;

    function openSidebar() {
        drawer.classList.add('is-open');
        drawer.setAttribute('aria-hidden', 'false');

        if (overlay) overlay.classList.add('is-open');
        document.body.style.overflow = 'hidden';

        const first = drawer.querySelector('button, a, [tabindex]');
        if (first) first.focus();
    }

    function closeSidebar() {
        drawer.classList.remove('is-open');
        drawer.setAttribute('aria-hidden', 'true');

        if (overlay) overlay.classList.remove('is-open');
        document.body.style.overflow = '';
    }

    // Слушаем событие закрытия из других частей кода
    document.addEventListener('sidebar:close', closeSidebar);

    document.querySelectorAll('[data-sidebar-open]').forEach(function (btn) {
        btn.addEventListener('click', openSidebar);
    });

    if (closeBtn) closeBtn.addEventListener('click', closeSidebar);
    if (overlay) overlay.addEventListener('click', closeSidebar);

    document.addEventListener('keydown', function (e) {
        if (e.key === 'Escape' && drawer.classList.contains('is-open')) {
            closeSidebar();
        }
    });

    document.querySelectorAll('.sidebar-accordion').forEach(function (btn) {
        btn.addEventListener('click', function () {
            btn.classList.toggle('is-open');
        });
    });
})();