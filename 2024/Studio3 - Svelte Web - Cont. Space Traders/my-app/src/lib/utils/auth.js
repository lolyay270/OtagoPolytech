/*
Utility functions for managing agent authentication details.

# File: auth.js
# Description: This module provides functions to manage agent authentication details, including retrieving and saving agent name and token from localStorage.

Example usage:
import { saveDetails, saveAgent, agentName, agentToken } from "$lib/utils/auth.js";
saveDetails(); // Retrieve agent details from localStorage
saveAgent("agent_name", "agent_token"); // Save agent details
console.log(agentName, agentToken); // Access agent details
*/

// Variables to store agent name and token
export let agentName = "";
export let agentToken = "";

// Function to retrieve and save agent details from localStorage
export function saveDetails() {
    // Retrieve agent details from localStorage
    const storedAgentName = localStorage.getItem("agentName");
    const storedAgentToken = localStorage.getItem("agentToken");

    // If agent details are found in localStorage, update variables
    if (storedAgentName && storedAgentToken) {
        agentName = storedAgentName;
        agentToken = storedAgentToken;
    }
}

// Function to save agent details to localStorage and update variables
export function saveAgent(name, token) {
    // Save agent details to localStorage
    localStorage.setItem("agentName", name);
    localStorage.setItem("agentToken", token);

    // Update variables
    agentName = name;
    agentToken = token;
}
