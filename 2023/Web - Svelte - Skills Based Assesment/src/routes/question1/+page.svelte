<script>

    /*

    Assemble the Avengers! (literally...)

        We have 2 data files at 2 different URLs - 1 has first names, and the 2nd has 
        last names.
        You need to assemble the full names of these Avengers and print them out in
        the ordered list below.

        e.g. 
        1. Tony Stark
        2. Bruce Banner

        etc etc

        The first names can be found at: https://gist.githubusercontent.com/dfenders/0fe9021b4242d83d973ac7087287e166/raw/8c0bda392b0381cfd254b719360e177fdf98b1ab/firstNames.json
        The last names can be found at: https://gist.githubusercontent.com/dfenders/0fe9021b4242d83d973ac7087287e166/raw/8c0bda392b0381cfd254b719360e177fdf98b1ab/lastNames.json

    */

    import { onMount } from "svelte";
    let avengers = [];
    const BASE_URL = `https://gist.githubusercontent.com/dfenders/0fe9021b4242d83d973ac7087287e166/raw/8c0bda392b0381cfd254b719360e177fdf98b1ab`

    onMount(async () => { 
        let res = await fetch(`${BASE_URL}/firstNames.json`); 
        avengers = await res.json(); 

        res = await fetch(`${BASE_URL}/lastNames.json`); 
        let data = await res.json(); 
        data.names.forEach((item, i) => { 
            avengers[i].name += ` ${item.name}`;
        });
    }); 

</script>

<ol>
    {#each avengers as human}
        <li>{human.name}</li>
    {/each}
</ol>