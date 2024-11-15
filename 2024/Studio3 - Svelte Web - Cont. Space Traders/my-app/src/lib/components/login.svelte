<!-- 
Svelte component for the login page.

# File: login.svelte
# Description: Component for user login functionality.

This component allows users to log in by entering their username and token. 
Upon successful login, it saves the agent details and redirects to the contracts page.

Example usage:
<Login />

-->

<script>
  import { saveAgent } from "$lib/utils/auth.js";

  let agentName = "";
  let agentToken = "";
  let message = "";

  // Function to handle user login
  async function login() {
    try {
      if (agentName && agentToken) {
        const res = await fetch("https://api.spacetraders.io/v2/my/agent", {
          method: "GET",
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${agentToken}`,
          },
        });

        const json = await res.json();

        if (
          res.ok &&
          json.data &&
          json.data.symbol === agentName.toUpperCase()
        ) {
          saveAgent(agentName, agentToken);
          message = "Login successful!";
          // Redirect to contracts page here if login is successful
          window.location.href = "/contracts";
        } else {
          throw new Error("Invalid username or token");
        }
      } else {
        message = "Please enter both username and token.";
      }
    } catch (error) {
      console.error(error);
      message = "Login failed";
    }
  }
</script>

<div class="container backgroundimg">
  <div class="box">
    <h1>Login</h1>
    <input
      class="nameinput"
      id="name"
      type="text"
      placeholder="Enter Your Username"
      bind:value={agentName}
    />
    <input
      class="tokeninput"
      id="token"
      type="text"
      placeholder="Enter Your Login Token"
      bind:value={agentToken}
    />
    
    <button class="button" on:click={login}>Let's Play !</button>


    {#if message}
      <p class="Error">{message}</p>
    {/if}
  </div>
</div>

