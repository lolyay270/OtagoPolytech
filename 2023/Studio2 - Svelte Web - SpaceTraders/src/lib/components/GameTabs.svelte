<script>
    import { onMount } from 'svelte';

    //Import the components here
    //import name from './name.svelte';
    import TestComponent from './TestComponent.svelte';
    import ContractMenu from './ContractMenu.svelte';
    import Systems from './Systems.svelte';

    //Add component and name to this array of objects

    const componentObjects = [
        {
            'name': 'Systems',
            'component': Systems
        },
        {
            'name': 'Contracts',
            'component': ContractMenu
        },
        {
            'name': 'Example 3',
            'component': TestComponent
        },
        {
            'name': 'Example 4',
            'component': TestComponent
        },
        {
            'name': 'Example 5',
            'component': TestComponent
        }
    ];

    let tabIndex = 0;
    let tabs = [];

    onMount (() => {
        FindAllTabs();
    });

    const FindAllTabs = () => {
        tabs = document.getElementsByClassName("tab");
    };

    const TabButtonClick = (index) => {
        tabs[tabIndex].classList.remove("tab-select");
        
        tabIndex = index;
        tabs[index].classList.add("tab-select");

        console.log(tabs);
    };
</script>


<div class="game-tabs">
    <ul class="tabs-list">
        {#each componentObjects as component, i (i)}
            <li class={i === tabIndex ? "tab tab-select" : "tab"}>
                <button on:click={() => TabButtonClick(i)}>{ component.name }</button>
            </li>
        {/each}
    </ul>
    <div class="tabs-display">
        <svelte:component this={componentObjects[tabIndex].component} />
    </div>
</div>