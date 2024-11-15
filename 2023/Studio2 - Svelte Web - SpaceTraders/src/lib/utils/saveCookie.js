const saveCookie = (cookieName, cookieValue) => {
    document.cookie = `${cookieName}=${cookieValue}`;
};

export default saveCookie;