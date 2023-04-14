const statsFormPopularity = document.getElementById("form-stats-popularity");
const statsResPopularity = document.getElementById("result-popularity");

const statsFormAge = document.getElementById("form-stats-age");
const statsResAge = document.getElementById("result-age");

const statsFormPerso = document.getElementById("form-stats-perso");
const statsResPerso = document.getElementById("result-perso");

statsFormPopularity.addEventListener("submit", event => {
    event.preventDefault();
    statsFormPopularity.style.display = "none"
    statsResPopularity.innerHTML = "Attente de la réponse du serveur..."
    fetch(`${window.location.protocol}//${window.location.host}/api/popularityStats`, {
        method: "post",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({})
    }).then(async (response) => {
        console.log(response.status)
        /*
        if (response.status !== response.ok) {
            statsRes.innerText = `Error ${response.status} : ${response.statusText}`;
            return;
        }
        */
        return response.json();
    }).then(data => {
        console.log(data);
        statsResPopularity.innerHTML = ""
        statsResPopularity.appendChild(createStatisticServerValueRepresentation(data))
    })
});

statsFormAge.addEventListener("submit", event => {
    event.preventDefault();
    statsFormAge.style.display = "none"
    statsResAge.innerHTML = "Attente de la réponse du serveur..."
    fetch(`${window.location.protocol}//${window.location.host}/api/ageStats`, {
        method: "post",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({})
    }).then(async (response) => {
        console.log(response.status)
        /*
        if (response.status !== response.ok) {
            statsRes.innerText = `Error ${response.status} : ${response.statusText}`;
            return;
        }
        */
        return response.json();
    }).then(data => {
        console.log(data);
        statsResAge.innerHTML = ""
        statsResAge.appendChild(createStatisticAgeValueRepresentation(data))
    })
});

statsFormPerso.addEventListener("submit", event => {
    event.preventDefault();
    statsFormPerso.style.display = "none"
    statsResPerso.innerHTML = "Attente de la réponse du serveur..."
    fetch(`${window.location.protocol}//${window.location.host}/api/persoStats`, {
        method: "post",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({})
    }).then(async (response) => {
        console.log(response.status)
        /*
        if (response.status !== response.ok) {
            statsRes.innerText = `Error ${response.status} : ${response.statusText}`;
            return;
        }
        */
        return response.json();
    }).then(data => {
        console.log(data);
        statsResPerso.innerHTML = ""
        statsResPerso.appendChild(createTemplateStatisticsData(data))
    })
});
