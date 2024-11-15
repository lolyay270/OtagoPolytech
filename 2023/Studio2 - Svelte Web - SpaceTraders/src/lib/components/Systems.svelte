<script>
    import { token } from "$lib/utils/stores.js";

    const options = {
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${$token}`,
        },
    };

    import { onMount } from "svelte"; 
    let systems = []; 
    const BASE_URL = `https://api.spacetraders.io/v2`;
    let randPage = Math.floor(Math.random() * 250);
    // number of systems varies, documentation says over 5k, so 5k (20 * 250) is max here to not get errors
    
    onMount(async () => { 
        let res = await fetch(`${BASE_URL}/systems?limit=20&page=${randPage}`, options);         //   limit max = 20, default = 10
        let data = await res.json(); 
        systems = data.data;
        
        let min_x = findMin(systems, "x");
        let max_x = findMax(systems, "x");
        let min_y = findMin(systems, "y");
        let max_y = findMax(systems, "y");

        // add the position to be used on screen to each system as a key/value pair
        systems.forEach((system, i) => {
            let position = { x : 0, y : 0 };

            position.x = positionCalculator(system.x, min_x, max_x);
            position.y = positionCalculator(system.y, min_y, max_y);

            system.position = position;
        });
        
        console.log(systems);
    }); 

    let findMin = (objArray, key) => {
        let min = 0;
        objArray.forEach(obj => {
            if (min > obj[key]) min = obj[key];
        })
        return min;
    }

    let findMax = (objArray, key) => {
        let max = 0;
        objArray.forEach(obj => {
            if (max < obj[key]) max = obj[key];
        })
        return max;
    }

    let positionCalculator = (value, min, max) => {
        if (min < 0) min = 0 - min;  // make min positive
        max += min;  
        value += min;
        return value / max * 95;
    }

</script>

<div class="grid">
    <div class="wrapper">
        {#each systems as system}
        <div style="left: {system.position.x}%; top: {system.position.y}%">
            <p>{system.symbol}</p> <!-- name of system show -->
            <div class="{system.type} starImage"/> <!-- img show  -->
            <p class="systemInfo {system.position.x > 80 ? "moveLeft" : ""} {system.position.y > 90 ? "moveUp" : ""}"> <!-- show only when hover -->
                Name: {system.symbol}<br/>
                Type: {system.type}<br/>
                Position: {system.x}, {system.y}<br/>
                Locations of Interest: {system.waypoints.length}
            </p>
        </div> 
        {/each}
    </div>
</div>