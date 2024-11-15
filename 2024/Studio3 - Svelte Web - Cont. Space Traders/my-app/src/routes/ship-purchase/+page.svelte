 <script>
//This code works, just commented out rn "SHIP_MINING_DRONE" & "X1-YT23-H52" will be
//imported from ship viewer page

// const options = {
//   method: 'POST',
//   headers: {
//     'Content-Type': 'application/json',
//     'Authorization': 'Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZGVudGlmaWVyIjoiS0tLS0tLS0siLCJ2ZXJzaW9uIjoidjIuMi4wIiwicmVzZXRfZGF0ZSI6IjIwMjQtMDMtMjQiLCJpYXQiOjE3MTIyMDIwNzEsInN1YiI6ImFnZW50LXRva2VuIn0.JQJXnFRJuMYVVqMMEwH7x4l3mO1haehH53pdJQ-EVhXllYZHJpisKLW-DrCCOYAzx7xCfvb0xzV-EVzFq_3ZyKaW5Qi1Hhmmksi5NKGu6bymDz7EblO9NqeyAbNVDgeqrcAVvj7om7Ydx9ZV8tVl2_aSI3IB3zRjVh1Ck3jMKsk8m82fybckQbHAM-_JH-L5ObiwY8bBkGyAsAhDtqB0WkvWsMk_WoQCQGVL5TKY4JORCjZxJ7cGuc9F2L4rHWhBkUgxNs9S1KmRvCf64z5cZ4Y3fLxNyOnWheqDbjjqA6AppJXbQ8bOr5HsKGNdH77WJIDMPO4DzrB0heniycIk0g'
//   },
//   body: JSON.stringify({
//     shipType: "SHIP_MINING_DRONE",
//     waypointSymbol: "X1-YT23-H52",
//   }),
// };

// fetch('https://api.spacetraders.io/v2/my/ships', options)
//   .then(response => response.json())
//   .then(response => console.log(response))
//   .catch(err => console.error(err));
import Header from "$lib/components/header.svelte";
// import {publicName} from "$lib/components/header.svelte";

</script>

  <Header />

  <div class="backgroundimg ships"> 
    <!-- this is just an example but check for however we export data
    to this page is < 0 or the string length is greater than 0 -->
    <!-- {#if shipView.length > 0} -->
    <div class="contract-box">
        <h2>Hello :Username:</h2>
        <h3>Thank you for your recent transaction</h3>
        <p>The ship below has been added to your inventory</p>
        <!-- All of this info will be pulled from contract viewer page
        these are just examples for how the css will look with the data inside -->
        <div>
          <p>SHIP NAME: yamazaki t4000</p>
          <p>SHIP CARGO: 0/50</p>
          <p>SHIP SPEED: 400</p>
          <p>FUEL: 99/99 </p>
          <p>AMOUNT PAID: $999999</p>
          <p>DATE ISSUED: 22/04/2024</p>
        </div>
    </div>

    <!-- {:else}
    <div class="contract-box">
      <div class="nameclass">
        <h1>No ship selected</h1>
        <p>Please return to (link to ship view page)</p>
        <p>and select a ship to buy</p>
      </div>
    </div>
  {/if} -->
  </div> 

 <style>
    .contract-box {
    width: 15%;
    height: auto;
    background-color: #829191; /* Content box colour */
    border-radius: 20px;
    padding: 20px; /* Adjust as necessary */
    margin: auto;
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    font-family: 'Roboto', sans-serif; /* Non-title font */
  }
  </style>