<!-- 
Svelte component for agent registration.

# File: register.svelte
# Description: Component for registering a new agent in the SpaceTraders game.

This component allows users to register a new agent by providing a username.
Upon successful registration, the agent's name and token are displayed.

Example usage:
<Register />

-->

<script>
  // Space Traders register agent code
  let publicToken;
  export let publicName = "TESTNAME"; // Default agent name
  let name;

  // Function to register a new agent
  function registerAgent() {
    // Get user input for agent name
    name = document.getElementById("userinput").value;

    // Fetch request to register agent with SpaceTraders API
    const options = {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        symbol: name,
        faction: "COSMIC", // Specify agent faction
      }),
    };

    fetch("https://api.spacetraders.io/v2/register", options)
      .then((response) => response.json())
      .then((response) => {
        saveToken(response);
        saveName(response);
      })
      .catch((err) => console.error(err));
  }

  // Function to save the agent's token
  function saveToken(response) {
    publicToken = response.data.token;
  }

  // Function to save the agent's name
  function saveName(response) {
    publicName = response.data.agent.symbol;
  }
</script>

<div class="container backgroundimg">
  <div class="box">
    <h1>Register</h1>
    <input
      class="input"
      id="userinput"
      type="text"
      placeholder="Enter a Username"
    />
    <a href="#popup">
      <button class="registerbtn" id="register" on:click={registerAgent}>
        Register agent
      </button>
    </a>
    <a href="/login/" class="login">Login</a>

    <div id="popup" class="overlay">
      <div class="popup">
        <a class="close" href="#">&times;</a>
        <div class="content">
          <h2>Agent Information</h2>
          <h3>Agent name</h3>
          <p>{publicName}</p>
          <h3>Agent token</h3>
          <p class="token-holder">{publicToken}</p>
        </div>
      </div>
    </div>
  </div>
</div>
