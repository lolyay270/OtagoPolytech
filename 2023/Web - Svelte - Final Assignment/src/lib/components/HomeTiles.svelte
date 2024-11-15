<script>
    const base_url = `https://api.thecatapi.com/v1`
const api_key = `live_yBWH0otnDLXMNNVIjdb7j6vV0F5tMFOUcbbOJ4mNDcKWnOdscPe99331duAJQ4kL`

    fetch(`${base_url}/images/search?limit=4&has_breeds=0&breed_ids=beng&api_key=${api_key}`)
    .then((response) => response.json())
    .then((data) => {
        data.forEach(cat => {
          console.log(cat.url)
        })
    })
    .catch(function(error) {
        console.log(error);
    });

    //list of breed codes i want to use
    //beng      birm        bomb        drex        mcoo        norw        rblu        tang
    //
</script>

