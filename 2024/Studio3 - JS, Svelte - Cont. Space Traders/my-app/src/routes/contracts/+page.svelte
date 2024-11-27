<!-- 
Svelte page component for displaying and accepting contracts.

# File: contracts/+page.svelte
# Description: Page component for displaying and accepting contracts in the SpaceTraders game.

This page component fetches contracts from the SpaceTraders API and displays them. It also allows the user to accept contracts by clicking a button.

Example usage:
<ContractsPage />
-->

<script>
  // Import Header component and onMount function from Svelte
  import Header from "$lib/components/header.svelte";
  import { onMount, afterUpdate } from "svelte";
  // Import AcceptContract component from lib directory
  import { AcceptContract } from "../../lib/components/accept-contract.svelte";

  // Variable to store contract details
  let contractDeets = [];

  // Function to fetch contracts from SpaceTraders API
  async function fetchContracts() {
    try {
      const token = localStorage.getItem("agentToken");
      if (!token) {
        throw new Error("Agent token not found.");
      }
      const options = {
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
      };

      const res = await fetch(
        "https://api.spacetraders.io/v2/my/contracts",
        options,
      );
      const json = await res.json();
      console.log("contracts info", json);
      contractDeets = json.data;
    } catch (error) {
      console.error("Error fetching contracts:", error);
    }
  }

  // Call fetchContracts function when the component mounts
  onMount(() => {
    fetchContracts();
  });

  // Function to handle button click event for accepting contracts
  async function buttonClick(givenId) {
    await new Promise((resolve) => { resolve(AcceptContract(givenId)); });  // Call the AcceptContract component with the contract ID
    await new Promise((resolve) => { resolve(fetchContracts()); });  //refill contractDeets with new data
    document.getElementById(`button${givenId}`).style.display = "none"; // Hide the accept button
  }

  afterUpdate(() => {
    fetchContracts(); //show the updated information on screen
  });
</script>

<!-- Display the Header component -->
<Header />

<div class="backgroundimg ships">
  {#if contractDeets.length > 0}
    {#each contractDeets as contract}
      <div class="contract-box">
        <div class="nameclass">
          <h2>Contract</h2>
          <!-- Display contract details -->
          <p>Job type: {contract.type}</p>
          <p>Deadline: {contract.deadlineToAccept}</p>
          {#key contract.accepted}
            <p>Accepted: {contract.accepted}</p>
          {/key}
          <p>Payment on accept: ${contract.terms.payment.onAccepted}</p>
          <p>Payment on complete: ${contract.terms.payment.onFulfilled}</p>
          <p>
            Resource: {contract.terms.deliver[0].tradeSymbol}
            {contract.terms.deliver[0].unitsFulfilled}/{contract.terms
              .deliver[0].unitsRequired}
          </p>
          <!-- Display accept button if contract is not accepted -->
          {#if contract.accepted != true}
            <button id="button{contract.id}" on:click={buttonClick(contract.id)}
              >Accept</button
            >
          {/if}
        </div>
      </div>
    {/each}
  {:else}
    <!-- Display message if no contracts are available -->
    <div class="contract-box">
      <div class="nameclass">
        <h2>Contract</h2>
        <p>No contracts currently available</p>
      </div>
    </div>
  {/if}
</div>

<style>
  .contract-box {
  width: 15%;
  height: auto;
  background-color: #829191; /* Content box colour */
  border-radius: 20px;
  padding: 20px; /* Adjust as necessary */
  margin: auto;
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
}

</style>
