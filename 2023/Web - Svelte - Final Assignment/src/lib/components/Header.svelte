<script>
    import Nav from "$lib/components/Nav.svelte";
    import { onMount } from "svelte";

    let isSticky = true;
    onMount(() => {
        document.addEventListener("wheel", (event) => {            
            isSticky = event.deltaY < 0;                     
        });
    })
    
</script>

<header class={isSticky ? 'sticky' : 'notSticky'}>
    <Nav />
    <div class="font-icons">
        <i class="fa-solid fa-cat"></i>
        <i class="fa-solid fa-cat"></i>
        <i class="fa-solid fa-cat"></i>
    </div>
    <h1 class="logo">Kitties</h1>
</header>