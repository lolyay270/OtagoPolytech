<script>
    export let data;
    console.log(data.cats);
</script>

<div class="container">
    {#each data.cats as cat}
        <div class="catGallery">
            <h2>{cat.breed}</h2>
            <div class="gallery">
                {#each cat.urls as url}
                    <img src={url} alt={cat.breed} />
                {/each}
            </div>
        </div>
    {/each}
</div>

<!-- inspiration:    https://www.coulisse-gallery.com/artists -->