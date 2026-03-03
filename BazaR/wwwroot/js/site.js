(function () {
    function openModal(id) {
        const el = document.getElementById(id);
        if (!el) return;
        el.classList.add("is-open");
        document.body.style.overflow = "hidden";
    }

    function closeModal(el) {
        el.classList.remove("is-open");
        document.body.style.overflow = "";
    }

    document.addEventListener("click", function (e) {
        // закриття
        const closeBtn = e.target.closest("[data-auth-close='true']");
        if (closeBtn) {
            const modal = e.target.closest(".auth-modal");
            if (modal) closeModal(modal);
            return;
        }

        // відкриття логіну
        const toLogin = e.target.closest("[data-open-login='true']");
        if (toLogin) {
            const login = document.getElementById("loginModal");
            const reg = document.getElementById("registerModal");
            if (reg) reg.classList.remove("is-open");
            if (login) openModal("loginModal");
            return;
        }

        // відкриття реєстрації
        const toReg = e.target.closest("[data-open-register='true']");
        if (toReg) {
            const login = document.getElementById("loginModal");
            const reg = document.getElementById("registerModal");
            if (login) login.classList.remove("is-open");
            if (reg) openModal("registerModal");
            return;
        }

        // перемикання видимості пароля
        const passToggle = e.target.closest("[data-pass-toggle='true']");
        if (passToggle) {
            const field = passToggle.closest(".auth-field--password");
            if (!field) return;
            const input = field.querySelector("input");
            if (!input) return;
            input.type = input.type === "password" ? "text" : "password";
        }
    });

    window.authModals = { openModal };
})();