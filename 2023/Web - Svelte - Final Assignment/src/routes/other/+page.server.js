export async function load() {
  const BASE_URL = "https://api.thecatapi.com/v1";
  const API_KEY = "live_yBWH0otnDLXMNNVIjdb7j6vV0F5tMFOUcbbOJ4mNDcKWnOdscPe99331duAJQ4kL";
  const CAT_IDS = [
    "beng",
    "birm",
    "bomb",
    "drex",
    "mcoo",
    "norw",
    "rblu",
    "tang",
  ];

  let cats = [];

  //fetch takes each id from CAT_IDS and gets 5 images and maps them to obj with id and breed
  const fetchData = async (id) => {
    const res = await fetch(
      `${BASE_URL}/images/search?limit=5&breed_ids=${id}&api_key=${API_KEY}`
    );
    const data = await res.json();

    let obj = {
      urls: [],
      id, //is shorthand for id: id,
      breed: data[0].breeds[0].name,
    };

    data.forEach((i) => {
      obj.urls.push(i.url);
    });
    cats.push(obj);
  };



  //I am using Promise.all to get only 1 response for all fetches for each cat id

  //google search: "how to fetch many times at once javascript"
  //opened: https://stackoverflow.com/questions/50381326/really-run-fetch-multiple-times-synchronously
  //which linked to https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Promise/all

  //given by Elise
  //https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/map

  await Promise.all(CAT_IDS.map((id) => fetchData(id)));

  return { cats };
}
