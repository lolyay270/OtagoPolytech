/*
Utility module for SpaceTraders API integration.

# File: index.js
# Description: This module provides functions for interacting with the SpaceTraders API, including accepting contracts.

Example usage:
import { AcceptContract } from '$lib/index.js';
AcceptContract('contract_id_here'); // Call the AcceptContract function with the contract ID
*/

// Options for the fetch request
const options = {
    method: 'POST',
    headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer your_access_token_here'
    },
};

/*
AcceptContract function accepts a contract in the SpaceTraders game.

@param {string} id - The ID of the contract to accept.
@return {Promise} - A Promise that resolves to the response from the SpaceTraders API.
*/
async function AcceptContract(id) {
    // Send a POST request to accept the contract
    fetch(`https://api.spacetraders.io/v2/my/contracts/${id}/accept`, options)
        .then(response => response.json())
        .catch(err => console.error(err));
}