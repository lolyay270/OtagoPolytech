<!-- 
Svelte component to handle accepting contracts.

# File: accept-contract.svelte
# Description: Component for accepting contracts in SpaceTraders game.

To use this component:
1. Import it in your Svelte file.
2. Call the AcceptContract function with the contract ID.

Example usage:
<script>
    import { AcceptContract } from '$lib/components/index.js';
    
    async function handleAcceptContract() {
        await AcceptContract('contract_id_here');
    }
</script>
<button on:click={handleAcceptContract}>Accept Contract</button>
-->

<script context="module">
  /* 
  Function to accept a contract by its ID
  Calls the SpaceTraders API to accept the contract with the provided ID
  Requires the agent token stored in localStorage
  */
  export async function AcceptContract(id) {
    let options;
    try {
      // Retrieve agent token from localStorage
      const token = localStorage.getItem("agentToken");
      if (!token) {
        throw new Error("Agent token not found.");
      }
      // Set up options for the fetch request
      options = {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
      };
    } catch (error) {
      console.error("Error fetching contracts:", error);
    }
    // Send request to accept the contract
    fetch(`https://api.spacetraders.io/v2/my/contracts/${id}/accept`, options)
      .then((response) => response.json())
      .catch((err) => console.error(err));
  }
</script>
