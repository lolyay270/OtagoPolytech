<script>

    import { onMount } from "svelte";
    let cats = [];

    const BASE_URL = `https://api.thecatapi.com/v1`
    const api_key = `live_yBWH0otnDLXMNNVIjdb7j6vV0F5tMFOUcbbOJ4mNDcKWnOdscPe99331duAJQ4kL`

    //onmount for getting 4 random cat imgs from specific cat ids 
    onMount(async () => { 
        let res = await fetch(`${BASE_URL}/images/search?limit=4&breed_ids=beng,birm,bomb,drex,mcoo,norw,rblu,tang&api_key=${api_key}`); 
    	cats = await res.json();
    })

    //https://api.thecatapi.com/v1/images/search?limit=4&breed_ids=beng,birm,bomb,drex,mcoo,norw,rblu,tang&api_key=live_yBWH0otnDLXMNNVIjdb7j6vV0F5tMFOUcbbOJ4mNDcKWnOdscPe99331duAJQ4kL
</script>

<!-- showing img and name for each cat in array -->
<div class="home_blocks">
    {#each cats as cat}
        <div>
            <div class="img_blocks" style="background-image: url({cat.url})"></div>
            <p>{cat.breeds[0].name}</p>
        </div>
    {/each}
</div>
