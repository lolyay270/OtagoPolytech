<!--
Svelte page component for the shipyard, displaying locations with shipyards and available ships for purchase.

# File: shipyard/+page.svelte
# Description: This page component fetches data about locations with shipyards and available ships for purchase from the SpaceTraders API. It allows users to select a location and view the ships available for purchase, along with their details.

This page component fetches data about locations with shipyards and available ships for purchase from the SpaceTraders API. It allows users to select a location and view the ships available for purchase, along with their details.

Example usage:
<shipyards />
-->

<script>
  // Importing necessary modules
  import { onMount } from "svelte";
  import Header from "$lib/components/header.svelte";
  import LoadingPage from "$lib/components/loadingPage.svelte";
  // Declaring variables to store fetched data and orbital information

  let waypoints = [];
  let placesWithShipyards = [];
  let selectedLocation = null;
  let shipsAtLocation = [];

  let startingSystem = "";

  // Options for API requests including authorization header
  const options = {
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${localStorage.getItem("agentToken")}`,
    },
  };


 async function Buyship(shiptype, waypoint){
const options = {
  method: 'POST',
  headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${localStorage.getItem("agentToken")}`,
    },
  body: JSON.stringify({
    shipType: `${shiptype}`,
    waypointSymbol: `${waypoint}`,
  }),
};

fetch('https://api.spacetraders.io/v2/my/ships', options)
  .then(response => response.json())
  .then(response => console.log(response))
  .catch(err => console.error(err));
}
  
    // Function to fetch agent data
  async function fetchAgent() {
    try {
      const response = await fetch(
        "https://api.spacetraders.io/v2/my/agent",
        options,
      );
      const agentData = await response.json();
      startingSystem = agentData.data.headquarters.replace(/-\w+$/, "");
    } catch (error) {
      console.error("Error fetching agent data:", error);
    }
  }

  // Function to fetch waypoints data
  async function fetchWaypoints() {
    try {
      // Fetching agent data
      await fetchAgent();
      // Fetching waypoints data for the current system with SHIPYARD trait
      const response = await fetch(
        `https://api.spacetraders.io/v2/systems/${startingSystem}/waypoints?traits=SHIPYARD`,
        options,
      );
      const fetchedWaypoints = await response.json();
      waypoints = fetchedWaypoints.data;
      console.log("System Data", waypoints);
      await filterShipyards();
    } catch (error) {
      console.error("Error fetching system data:", error);
    }
  }

  // Function to filter shipyards
  async function filterShipyards() {
    // Filtering places with shipyards
    placesWithShipyards = waypoints
      .filter((place) => {
        return place.traits.some((trait) => trait.symbol === "SHIPYARD");
      })
      .map((place) => {
        // Mapping over filtered places to extract relevant shipyard information
        const shipyardTrait = place.traits.find(
          (trait) => trait.symbol === "SHIPYARD",
        );
        return {
          symbol: place.symbol,
          type: place.type,
          shipyard: shipyardTrait,
        };
      });
    console.log("Locations with shipyards", placesWithShipyards);
  }

  // Function to fetch ships data at a location
  async function fetchShipsAtLocation(locationSymbol) {
    try {
      // Fetching ships data for the selected location
      const response = await fetch(
        `https://api.spacetraders.io/v2/systems/${startingSystem}/waypoints/${locationSymbol}/shipyard`,
        options,
      );
      console.log(startingSystem, locationSymbol);
      const shipsData = await response.json();
      // Checking if ships data is available and mapping over ships to add showDetails property
      shipsAtLocation = shipsData.data.ships
        ? shipsData.data.ships.map((ship) => ({ ...ship, showDetails: false }))
        : [];
      console.log("Ships at Location", shipsAtLocation);
    } catch (error) {
      console.error("Error fetching ships data:", error);
    }
  }

  // Function to handle location selection
  function selectLocation(locationSymbol) {
    selectedLocation = locationSymbol;
    fetchShipsAtLocation(locationSymbol);
  }

  // Function to toggle ship details visibility
  function toggleShipDetails(ship) {
    ship.showDetails = !ship.showDetails;
    // Refreshing shipsAtLocation array to reflect changes
    shipsAtLocation = [...shipsAtLocation];
  }

  // Lifecycle hook to fetch waypoints data on component mount
  onMount(() => {
    fetchWaypoints();
  });
</script>

<!-- Header component -->
<Header />

<!-- Main content -->
<div class="content">
  {#if placesWithShipyards.length > 0}
    <!-- Displaying locations with shipyards -->
    <div class="emptySpace"></div>
    <ul class="container">
      <h1>Locations</h1>
      {#each placesWithShipyards as place}
        <li>
          <!-- Button to select a location -->
          <button on:click={() => selectLocation(place.symbol)}
            >{place.type} - ({place.symbol})</button
          >
        </li>
      {/each}
    </ul>
    <!-- Displaying ships at the selected location -->
    <ul class="container">
      {#if selectedLocation}
        <h1>Ships at {selectedLocation}</h1>
        {#if shipsAtLocation.length > 0}
          {#each shipsAtLocation as ship}
            <li>
              <!-- Button to toggle ship details visibility -->
              <button on:click={() => toggleShipDetails(ship)}
                >{ship.name} - {ship.type}</button
              >
              {#if ship.showDetails}
                <!-- Displaying ship details -->
                <div class="shipDetails">
                  <p>Cost: ${ship.purchasePrice}</p>
                  <p>Activity: {ship.activity}</p>
                  <p>Crew: {ship.crew.required}</p>
                  <p>Description: {ship.description}</p>
                  <p>Engine: {ship.engine.name}</p>
                  <p>Frame: {ship.frame.name}</p>
                  <button on:click={async () => await Buyship(ship.type, selectedLocation)}>Buy Ship</button>
                </div>
              {/if}
            </li>
          {/each}
        {:else}
          <h2>No ships available at this location.</h2>
        {/if}
      {:else}
        <h1>No location selected</h1>
      {/if}
    </ul>
    <div class="emptySpace"></div>
  {:else}
    <!-- Display loading page if data is not available -->
    <LoadingPage></LoadingPage>
  {/if}
</div>

<style>
  .content {
    height: 100vh;
    background-image: url("/images/loginBackgroud1.png");
    background-position: center;
    background-repeat: no-repeat;
    background-size: cover;
    display: flex;
    flex-direction: row;
    align-items: center;
    justify-content: center;
    gap: 10%;
  }

  .container {
    width: 100%;
    height: 80%;
    background-color: #829191;
    background-image: none;
    padding: 0;
    margin: 6vh 0 0 0;
    border-radius: 4%;
    list-style: none;
  }

  .emptySpace {
    flex: 1;
  }

  p {
    font-family: "Oswald", sans-serif;
    font-size: 2rem;
    color: #c5c5c5;
  }

  button {
    background-color: #949B96;
    color: black;
    font-weight: bold;
  }

  li {
    padding-bottom: 10%;
    font-size: 1vw;
    font-family: "Roboto", sans-serif;
  }

  .shipDetails > p {
    font-size: 19px;
    color: #c5c5c5; /* Ensure font color is consistent */
  }

  @media (max-width: 1300px) {
    .content {
      gap: 5vw;
    }

    .container {
      padding: 2vh 0 2vh 0;
    }

    li {
      font-size: 1.5vw;
    }

    p {
      font-size: 4vw;
    }
  }

  @media (max-width: 1000px) {
    .content {
      flex-direction: column;
      gap: 0;
    }

    li {
      font-size: 4vw;
    }

    p {
      font-size: 4vw;
    }
  }
</style>
