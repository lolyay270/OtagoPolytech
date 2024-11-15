<!-- 
    Layout Component: layout.svelte
    
    Description: 
    This layout component serves as the template for all pages in the application. 
    It includes common elements such as CSS stylesheets, Google Fonts, favicon and <title>.
-->

<script>
    import {page} from '$app/stores'
    import { beforeNavigate } from '$app/navigation';
    
    let pageName = "";
    
    //-----code to change <title> depending on what page the player is on----//
    UrlToName($page.url.pathname) //change on page refresh 

    beforeNavigate((navigation) => { //change on url change (clicking header links)
        UrlToName(navigation.to.route.id);
    })
    
    function UrlToName(url) { //changes "/location" into "Location", checks for home page "/"
        let pageUrl = url;
        pageName = pageUrl.slice(1) //remove "/" at start
        pageName = pageName.charAt(0).toUpperCase() + pageName.slice(1); //make first letter capital
        if (pageName.length === 0) {
            pageName = "Home";
        }
    }
</script>

<svelte:head>
    <!-- Link to the main CSS file -->
    <link rel="stylesheet" href="css/main.css"/>

    <!-- Importing Google Fonts for consistent typography -->
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Oswald&family=Roboto&display=swap" rel="stylesheet">

    <!-- importing favicon images in all sizes/file types for different browsers -->
    <link rel="icon" type="image/png" sizes="16x16" href='images/favicon-16x16.png'>
    <link rel="icon" type="image/png" sizes="32x32" href='images/favicon-32x32.png'>
    <link rel="icon" type="image/x-icon" href='images/favicon.ico'>

    <title>SpaceTraders - {pageName}</title>
</svelte:head>

<!-- Slot for rendering the content of individual pages -->
<slot />
