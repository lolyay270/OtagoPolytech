const getCookie = (cookieName) => {
    const allCookies = document.cookie;
    const cookieList = allCookies.split(";");

    let cookieValue;
    cookieList.forEach(cookie => { if (cookie.includes(`${cookieName}=`)) cookieValue = cookie.slice(cookie.indexOf(`=`) + 1) })

    return cookieValue;
};

export default getCookie;