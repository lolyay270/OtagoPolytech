<script>
    import {onMount} from "svelte";
    import {token} from "$lib/utils/stores.js";
    

    let contractInfo = [];

    onMount(async() => {
        const options = {
        headers: {
            'Content-Type': `application/json`,
            'Authorization': `Bearer ${$token}`
        },
        };

        let resContractInfo = await fetch('https://api.spacetraders.io/v2/my/contracts', options);
        let dataContractInfo  = await resContractInfo.json();
        contractInfo = dataContractInfo.data;
        console.log(contractInfo);
    }) 
    
    let accept = async(contractID) => {
        const options = {
        method: 'POST',
        headers: {
            'Content-Type': `application/json`,
            'Authorization': `Bearer ${$token}`
        },
        };
        
        console.log(contractID);

        
        console.log(contractID);
        let resAccept = await fetch(`https://api.spacetraders.io/v2/my/contracts/${contractID}/accept`, options);
        let dataAccept = await resAccept.json();
        console.log(dataAccept);
    }

    let deliver = async(contractID) => {
        let resDeliver = await fetch();
    }
</script>

<div class="contractMenu">
    {#each contractInfo as contract}
    <div class = "contract">
        <h3>Contract type: {contract.type}</h3>
        <h3>Deadline: {contract.terms.deadline}</h3>
        <h3>Payment:</h3>
        <p>On accept: {contract.terms.payment.onAccepted}C  On complete: {contract.terms.payment.onFulfilled}C</p>
        <h3>Contract requirements:</h3>
        {#each contract.terms.deliver as deliver}
            <p>Deliver {deliver.unitsRequired} {deliver.tradeSymbol} to {deliver.destinationSymbol}</p>
            <p>You have delivered {deliver.unitsFulfilled} {deliver.tradeSymbol}</p>
        {/each}

        <!--Contract accept/submit button-->
        {#if !contract.accepted}
            <button on:click = {() => accept(contract.id)}>Accept</button>
        {:else}
            <button on:click = {() => deliver(contract.id)}>Deliver</button>
        {/if}
        
    </div>
    {/each}
</div>
