<script>
  import { loggedIn, token } from "$lib/utils/stores.js";
  import getCookie from "$lib/utils/getCookie.js";
  import saveCookie from "$lib/utils/saveCookie.js";
  import { onMount } from "svelte";

  export let username;

  const fetchAgent = async () => {
    const loginOptions = {
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${$token}`,
      },
    };

    const res = await fetch(
      "https://api.spacetraders.io/v2/my/agent",
      loginOptions
    );

    if (!res.ok) {
      console.log("Username or token is incorrect!");
      return;
    }

    return await res.json();
  };

  const login = async () => {
    if (username.length === 0 || $token.length === 0) return;
    
    const data = await fetchAgent();
    console.log(data);

    if (!data) return;

    if (data.data.symbol !== username.toUpperCase()) {
      console.log("Username or token is incorrect!");
      return;
    }

    saveCookie("token", $token);

    $loggedIn = true;
    console.log("Login successful!");
  };

  const skipLogin = async () => {
    let tokenFromCookie = getCookie("token");

    if (tokenFromCookie) {
      $token = tokenFromCookie;
      const data = await fetchAgent();

      if (!data) {
        console.log("Something went wrong getting user info!");
        return;
      }

      username = data.data.symbol;

      $loggedIn = true;
    }
  };

  onMount(async () => {
    await skipLogin();
  });
</script>

<div class="login">
  <h1>Login</h1>
  <form on:submit|preventDefault={login} id="login-form">
    <input id="username" type="text" placeholder="Username" bind:value={username} />
    <input id="token" type="text" placeholder="Token" bind:value={$token} />
    <button>SIGN IN</button>
  </form>
</div>
