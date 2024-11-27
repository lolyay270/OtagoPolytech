<!-- 
Svelte component for the loading page.

# File: loadingPage.svelte
# Description: Component for displaying a loading indicator during page transitions.

This component displays a simple loading message while the page is loading.

Example usage:
<LoadingPage />

-->

<p id="loading">Loading...</p>

<style>
    p#loading {
        font-family: "Oswald", sans-serif;
        font-size: 4vw;
        font-weight: 500;
        text-shadow: 0.2vw 0.1vw #2c423f;
    }
</style>
