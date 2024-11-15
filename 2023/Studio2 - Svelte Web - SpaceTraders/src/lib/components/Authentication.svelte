<script>
    import Login from "$lib/components/Login.svelte";
    import Register from "./Register.svelte";

    let username = "";
    let tab = 0;
</script>

<div class="auth-bg">
    <div class="auth-container">
        <div class="auth-content">
            {#if tab === 0}<Login bind:username={username} />{/if}
            {#if tab === 1}<Register bind:username={username} />{/if}
        </div>
        <div class="auth-change">
            {#if tab === 0}<button on:click={() => tab = 1}>Register</button>{/if}
            {#if tab === 1}<button on:click={() => tab = 0}>Login</button>{/if}
        </div>
    </div>
</div>
