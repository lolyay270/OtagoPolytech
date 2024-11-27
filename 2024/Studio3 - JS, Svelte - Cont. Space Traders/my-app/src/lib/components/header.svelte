<!-- 
Svelte component for the header section of the application.

# File: Header.svelte
# Description: Component for displaying the header/navigation bar of the Space Pirates application.

This component fetches agent data from the SpaceTraders API upon mounting, including the agent's credits and public name.

Example usage:
<Header />

-->

<script>
  import { onMount } from "svelte";

  let credits; // Variable to store agent credits
  export let publicName; // Public name of the agent

  // Function to fetch agent data from the API
  async function fetchAgentData() {
    const token = localStorage.getItem("agentToken");

    if (!token) {
      console.error("Agent token not found.");
      return;
    }

    const options = {
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
    };

    try {
      const response = await fetch(
        "https://api.spacetraders.io/v2/my/agent",
        options,
      );
      const data = await response.json();
      saveToken(data);
      saveName(data);
    } catch (error) {
      console.error(error);
    }
  }

  // Function to save agent's credits from the response
  function saveToken(response) {
    credits = response.data.credits;
  }

  // Function to save agent's public name from the response
  function saveName(response) {
    publicName = response.data.symbol;
  }

  // Fetch agent data upon component mounting
  onMount(fetchAgentData);
</script>

<header>
  <h1 class="logo">SPACE PIRATES</h1>
  <nav>
    <ul class="nav__links">
      <li><a href="/location">Locations</a></li>
      <li><a href="/contracts">Contracts</a></li>
      <li><a href="/shipyards">Shipyards</a></li>
    </ul>
  </nav>
  <p class="nameclass">Name: {publicName}</p>
  <p class="credclass">${credits}</p>
  <a class="cta" href="/ships"><button>Help</button></a>
</header>
