<!--
Svelte page component for the registration of agents.

# File: register/+page.svelte
# Description: This page component allows users to register new agents by providing a username. It interacts with the SpaceTraders API to register new agents and displays the agent's name and token upon successful registration.

This page component allows users to register new agents by providing a username. It interacts with the SpaceTraders API to register new agents and displays the agent's name and token upon successful registration.
-->

<script>
  // Import Register component
  import Register from "$lib/components/register.svelte";
</script>

<!-- Display the Register component -->
<Register />
