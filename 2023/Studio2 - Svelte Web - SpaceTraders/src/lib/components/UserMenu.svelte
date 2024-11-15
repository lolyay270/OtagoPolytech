<script>
    import getCookie from "$lib/utils/getCookie.js";
    import saveCookie from "$lib/utils/saveCookie.js";

    let menu = false;

    const logout = () => {
        saveCookie("token", " ;expires=Thu, 01 Jan 1970 00:00:00 UTC;");
        location.reload();
    };

    const openMenu = () => {
        menu = true;
        console.log("open user menu");
    };

    const closeMenu = () => {
        menu = false;
    };

    const copyToken = () => {
        let token = getCookie("token");
        navigator.clipboard.writeText(token);

        let copy = document.getElementById("copyMessage");
        copy.classList.add("shown");
        setTimeout(() => { copy.classList.remove("shown"); }, 2000);
    };
</script>

<a id="userIcon" href="/" on:click={openMenu} title="User">
    <svg xmlns="http://www.w3.org/2000/svg" height="2em" viewBox="0 0 448 512"><!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. --><path d="M224 256A128 128 0 1 0 224 0a128 128 0 1 0 0 256zm-45.7 48C79.8 304 0 383.8 0 482.3C0 498.7 13.3 512 29.7 512H418.3c16.4 0 29.7-13.3 29.7-29.7C448 383.8 368.2 304 269.7 304H178.3z"/></svg>
</a>

{#if menu}
    <button class="closeMenu" on:click={closeMenu}/>
    <div class="user">
        <div class="user-content">
            <p><a href="/" on:click={copyToken}>Copy token</a></p>
            <p><a href="/" on:click={logout}>Log out</a></p>
        </div>
    </div>
    <div id="copyMessage">
        <p>Copied to clipboard! :D</p>
    </div>
{/if}