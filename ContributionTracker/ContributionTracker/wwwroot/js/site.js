
    const body = document.querySelector("body"),
    navBar = document.querySelector("nav"),
    menuBtns = document.querySelectorAll(".menu-icon"),
    overlay = document.querySelector(".overlay"),
    modeSwitch = body.querySelector(".toggle-switch"),
    modeText = body.querySelector(".mode-text");

        menuBtns.forEach((menuBtn) => {
        menuBtn.addEventListener("click", () => {
            navBar.classList.toggle("open");
        });
        });

        overlay.addEventListener("click", () => {
        navBar.classList.remove("open");
        });
