<script>
    import { token } from "$lib/utils/stores.js";

    export let username;
  
    const fetchAgent = async () => {
        const registerOptions = {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                symbol: username,
                faction: "COSMIC",
            }),
        };
    
        const res = await fetch(
            "https://api.spacetraders.io/v2/register",
            registerOptions
        );
    
        if (!res.ok) {
            console.log("Username is taken!");
            return;
        }
    
        return await res.json();
    };
  
    const register = async () => {
        if (username.length === 0) return;
        
        const data = await fetchAgent();
        console.log(data);
    
        if (!data) return;

        $token = data.data.token;

        $token.length !== 0 ? console.log("User successfully registered!") : console.log("Something went wrong! Please try again later.");
    };
</script>

<div class="register">
    <h1>Register</h1>
    <form on:submit|preventDefault={register} id="register-form">
        <input id="username" type="text" placeholder="Username" bind:value={username} />
        <input id="token" type="text" placeholder="Token provided after registration" bind:value={$token} readonly>
        <button>SIGN UP</button>
    </form>
</div>