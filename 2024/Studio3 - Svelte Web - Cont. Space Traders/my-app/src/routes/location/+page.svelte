<!--
Svelte page component for displaying information about locations and orbitals.

# File: contracts/+page.svelte
# Description: This page component fetches data about a specific system and its associated planets and orbitals. It displays information about the system, planets, and orbitals, including their coordinates, structures, and environmental traits.

This page component fetches data about a specific system and its associated planets and orbitals. It displays information about the system, planets, and orbitals, including their coordinates, structures, and environmental traits.

Example usage:
<LocationPage />
-->

<script>
    // Import Header component and onMount function from Svelte
    import Header from "$lib/components/header.svelte";
    import { onMount } from "svelte";

    // Variables to store fetched data and orbital information
    let planetData;
    let systemData;
    let orbitalData;
    let orbitals = [];
    let planetTraits = [];
    let showPlanetMore = false;

    // Constants for API and authentication
    const BASE_URL = "https://api.spacetraders.io/v2";

    // Starting system and waypoint
    let startingSystem = ""; 
    let startingWaypoint = "";

    // Function to find the system the agent is in
    async function fetchCurrentSystem() {
        try { 
          const response = await fetch(
            "https://api.spacetraders.io/v2/my/agent",
            getOptions(),
          );
          const agentData = await response.json();
          startingWaypoint = agentData.data.headquarters;
          startingSystem = agentData.data.headquarters.replace(/-\w+$/, ""); //removing "-A1" from end
        } catch (error) {
            console.error("Error fetching system name:", error);
        }
    }


    // Function to fetch data about the current system
    async function fetchSystemData() {
        try {
            // Fetch data for the current system from the API
            const response = await fetch(
                `${BASE_URL}/systems/${startingSystem}`,
                getOptions(),
            );
            const fetchedDataSystems = await response.json();
            systemData = fetchedDataSystems.data;
            console.log("System Data", systemData);
        } catch (error) {
            console.error("Error fetching system data:", error);
        }
    }

    // Function to fetch data about the current planet
    async function fetchPlanetData() {
        try {
            // Fetch data for the current planet from the API
            const response = await fetch(
                `${BASE_URL}/systems/${startingSystem}/waypoints/${startingWaypoint}`,
                getOptions(),
            );
            const fetchedDataPlanets = await response.json();
            planetData = fetchedDataPlanets.data;
            console.log("Planet Data", planetData);

            // Store traits of the planet
            planetTraits = planetData.traits;

        } catch (error) {
            console.error("Error fetching planet data:", error);
        }
    }

    // Function to fetch orbital data for each orbital associated with the planet
    async function fetchOrbitalData() {
        try {
            // Check if planetData is available
            if (!planetData) {
                throw new Error("Planet data not available.");
            }

            // Fetch data for each orbital associated with the planet
            await Promise.all(
                planetData.orbitals.map(async (orbital) => {
                    // Fetch orbital data from the API
                    const orbitalResponse = await fetch(
                        `${BASE_URL}/systems/${startingSystem}/waypoints/${orbital.symbol}`,
                        getOptions(),
                    );
                    orbitalData = await orbitalResponse.json();

                    // Extract relevant orbital information
                    let orbitalInfo = orbitalData.data;

                    // Rearrange orbital traits for better display (if needed)
                    orbitalInfo.traits.forEach((trait, i) => {
                        if (
                            trait.name === "Shipyard" &&
                            orbitalInfo.traits.length >= 2
                        ) {
                            orbitalInfo.traits = swapItems(
                                orbitalInfo.traits,
                                i,
                                1,
                            );
                        }

                        if (
                            trait.name === "Marketplace" &&
                            orbitalInfo.traits.length >= 3
                        ) {
                            let indexToSwap = 2;
                            if (
                                !orbitalInfo.traits.find(
                                    (o) => o.name === "Shipyard",
                                )
                            ) {
                                indexToSwap = 1;
                            }
                            orbitalInfo.traits = swapItems(
                                orbitalInfo.traits,
                                i,
                                indexToSwap,
                            );
                        }
                    });

                    // Create a boolean flag to control whether to show more traits
                    orbitalInfo.showMore = false;

                    // Push orbital information to the orbitals array
                    orbitals = [...orbitals, orbitalInfo];
                }),
            );
            console.log("OrbitalsData", orbitals);
        } catch (error) {
            console.error("Error fetching orbital data:", error);
        }
    }

    // Function to swap items in a list
    function swapItems(list, indexA, indexB) {
        let temp = list[indexA];
        list[indexA] = list[indexB];
        list[indexB] = temp;
        return list;
    }

    // Function to determine priority traits
    function priorityTraits(name) {
        switch (name) {
            case "Shipyard":
            case "Marketplace":
            case "Outpost":
                return true;
            default:
                return false;
        }
    }

    // Function to toggle showing more traits for the planet
    function toggleShowPlanetTraits() {
        showPlanetMore = !showPlanetMore;
    }

    // Function to toggle showing more traits for orbitals
    function toggleShowOrbitalTraits(i) {
        orbitals[i].showMore = !orbitals[i].showMore;
    }

    // Function to generate options with Authorization header
    function getOptions() {
        try {
            const token = localStorage.getItem("agentToken");
            if (!token) {
                throw new Error("Agent token not found.");
            }
            return {
                headers: {
                    "Content-Type": "application/json",
                    Authorization: `Bearer ${token}`,
                },
            };
        } catch (error) {
            console.error("Error fetching contracts:", error);
        }
    }

    async function OrderOfFunctions() {
        await fetchCurrentSystem();
        await fetchSystemData();
        await fetchPlanetData();
        fetchOrbitalData();
    }
    // Lifecycle hook to fetch system data on component mount
    onMount(() => {
        OrderOfFunctions();
    });
</script>

<!-- Display the Header component -->
<Header />

<!-- Displaying waypoint data -->
<div class="container">
    {#if planetData != null}
        <!-- Display system and planet data -->
        <div class="system-info">
            <h1>System - {systemData.symbol}</h1>
            <p>Coordinates: X = {systemData.x} Y = {systemData.y}</p>
        </div>

        <!-- Planet data container -->
        <div class="content">
            <div class="planet-container">
                <h1>Planet - {planetData.symbol}</h1>
                <ul>
                    <h3>Structures</h3>
                    {#each planetTraits as trait, i}
                        {#if priorityTraits(trait.name)}
                            <li class="structures">
                                <h2>{trait.name}</h2>
                                <p>{trait.description}</p>
                            </li>
                        {/if}
                    {/each}
                    {#if showPlanetMore}
                        <li class="environmental">
                            <h2>Environmental</h2>
                            {#each planetTraits as trait, i}
                                {#if !priorityTraits(trait.name)}
                                    <h2>{trait.name}</h2>

                                    <p>{trait.description}</p>
                                {/if}
                            {/each}
                        </li>
                    {:else}
                        <button on:click={toggleShowPlanetTraits}
                            >Show More Planet Traits</button
                        >
                    {/if}
                </ul>
            </div>

            <!-- Orbital data container -->
            <div class="orbitals-container">
                <!--<h2>Orbitals</h2>-->
                {#each orbitals as orb, i}
                    <div class="orbital-container">
                        <h1>{orb.type} - {orb.symbol}</h1>
                        <p>Coordinates: X = {orb.x} Y = {orb.y}</p>
                        <ul>
                            <h3>Structures</h3>
                            {#each orb.traits as trait, j}
                                {#if priorityTraits(trait.name)}
                                    <li class="structures">
                                        <h2>{trait.name}</h2>
                                        <p>{trait.description}</p>
                                    </li>
                                {/if}
                            {/each}
                            {#if orb.showMore}
                                <li class="environmental">
                                    <h2>Environmental</h2>
                                    {#each orb.traits as trait, j}
                                        {#if !priorityTraits(trait.name)}
                                            <h2>{trait.name}</h2>
                                            <p>{trait.description}</p>
                                        {/if}
                                    {/each}
                                </li>
                            {/if}
                        </ul>
                        {#if orb.traits.length >= 3}
                            <button on:click={() => toggleShowOrbitalTraits(i)}>
                                {#if orb.showMore}
                                    Show Less Orbital Traits
                                {:else}
                                    Show More Orbital Traits
                                {/if}
                            </button>
                        {/if}
                    </div>
                {/each}
            </div>
        </div>
    {:else}
        <!-- Display loading message if data is not available -->
        <p>Loading data...</p>
    {/if}
</div>

<!-- Styling -->
<style>
    .content {
        height: 100%;
        display: flex;
        flex-direction: row;
        justify-content: center;
    }

    .container {
        display: flex;
        flex-direction: column;
        padding-top: 20px;
        height: 100%;
        align-items: center;
        overflow-y: auto;
        background-image: url("../images/loginBackgroud1.png");
        background-size: cover;
    }

    .system-info {
        margin-top: 5%; /* Adjust margin-top as needed */
        background-color: #829191;
        padding: 20px;
        border-radius: 15px;
        width: 40%; /* Adjust width as needed */
        align-items: center;
        margin: 7%; /* Add margin to separate from the right side container */
    }

    @media (max-width: 768px) {
        .system-info {
            width: 100%; /* Full width on smaller screens */
            margin-right: 0; /* Remove margin on smaller screens */
        }
    }

    .planet-container,
    .orbital-container {
        background-color: #829191;
        padding: 20px;
        margin-bottom: 10px;
        border-radius: 15px;
        width: 80%;
        margin: 40px; /* Adjust margin-top for visual balance */
    }

    .orbitals-container {
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
        gap: 20px;
        width: 100%;
    }

    button {
        background-color: #949b96;
        color: black;
        font-weight: bold;
    }

    p,
    .structures {
        font-family: "Roboto", sans-serif;
        color: #c5c5c5;
    }

    h1:after,
    h3:after {
        content: " ";
        display: block;
        border: 2px solid rgb(102, 102, 102);
    }

    h2:after {
        display: block;
        border: 2px solid rgb(255, 255, 255);
    }

    .structures {
        border-top: 2px solid #ccc;
        margin-top: 10px;
        padding-top: 10px;
        list-style: none;
    }

    .structures h4 {
        margin-bottom: 5px;
        list-style: none;
    }

    .structures p {
        margin-top: 5px;
    }

    @media (max-width: 768px) {
        .content {
            flex-direction: column; /* Change to column layout on smaller screens */
            gap: 5%;
        }

        .planet-container,
        .orbitals-container {
            width: 100%; /* Full width on smaller screens */
        }
    }
</style>
