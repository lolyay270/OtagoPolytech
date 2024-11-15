import { writable } from "svelte/store";

const loggedIn = writable(false);
const token = writable("");
const user = writable({});

export {
  loggedIn,
  token,
  user
};