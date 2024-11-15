<script>
    import AgentInfo from "../lib/components/AgentInfo.svelte";
    import GameTabs from "../lib/components/GameTabs.svelte";
    import Header from "$lib/components/Header.svelte";
    import Footer from "../lib/components/Footer.svelte"
    import Authentication from "$lib/components/Authentication.svelte";
    import { loggedIn, token } from "$lib/utils/stores.js";
</script>

<div class="BG"> 
    {#if !$loggedIn}
        <Authentication />
    {:else}
        <Header />
        <AgentInfo/>
        <div class="tabs-container">
          <GameTabs/>
        </div>
        <Footer />
    {/if}
</div>