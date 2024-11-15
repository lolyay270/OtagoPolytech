<script>
    import { token, user } from "$lib/utils/stores.js";
    import { onMount } from 'svelte';
    import UserMenu from "$lib/components/UserMenu.svelte";

    const options = {
    headers: {
        'Content-Type': `application/json`,
        'Authorization': `Bearer ${$token}`
    },
    };

    onMount(async() => {
        if ($token.length === 0) return;

        let resAgentInfo = await fetch('https://api.spacetraders.io/v2/my/agent', options);
        let dataAgentInfo = await resAgentInfo.json();
        $user = dataAgentInfo.data;
    })

</script>

<div class = "agentInfo">
    <UserMenu />
    <h1>{$user.symbol}</h1>
    <ul>
        <li>Credits: {$user.credits}</li>
        <li>Ships: {$user.shipCount}</li>
        <li>Headquarters: {$user.headquarters}</li>
    </ul>
</div>