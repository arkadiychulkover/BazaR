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

    document.querySelectorAll('.auth-pass-toggle').forEach(function (button) {
        button.addEventListener('click', function () {
            const wrap = button.closest('.auth-input-wrap');
            const input = wrap ? wrap.querySelector('input[type="password"], input[type="text"]') : null;

            if (!input) return;

            input.type = input.type === 'password' ? 'text' : 'password';
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