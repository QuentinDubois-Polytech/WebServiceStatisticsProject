function createTemplateStatisticsData(StatsData) {
    console.log(StatsData)
    let container = document.createElement("div")
    let nbCookiesSetAverageRecovered = document.createElement("p")
    let nbSecondsFromModificationAverageRecovered = document.createElement("p")
    let IsAltSvcAverageRecovered = document.createElement("p")
    
    nbCookiesSetAverageRecovered.innerHTML = `<b/>Nombre moyen de cookies : </b> ${Math.round(StatsData.nbCookiesSetAverage * 10) / 10} `
    nbSecondsFromModificationAverageRecovered.innerHTML = `<b/>Ancienneté moyenne des pages consultés : </b> ${StatsData.nbSecondsFromModificationAverage} secondes`
    IsAltSvcAverageRecovered.innerHTML = `<b/>Alt-Svc : </b> ${Math.round(StatsData.IsAltSvcAverage * 100 * 10) / 10}%`

    let fragment = document.createDocumentFragment()
    fragment.appendChild(nbCookiesSetAverageRecovered)
    fragment.appendChild(nbSecondsFromModificationAverageRecovered)
    fragment.appendChild(IsAltSvcAverageRecovered)

    container.appendChild(fragment)

    return container
}

function createStatisticAgeValueRepresentation(ageStats) {
    console.log(ageStats)
    let ageStatsContainer = document.createElement("div")
    let ageStatsAverageParagraph = document.createElement("p")
    let ageStatsEcartParagraph = document.createElement("p")

    ageStatsAverageParagraph.innerHTML = `<b/>Temps moyen en secondes : </b> ${ageStats.averageAge}`
    ageStatsEcartParagraph.innerHTML = `<b/>Ecart type du temps en secondes : </b> ${ageStats.ecartTypeAge}`
    
    let fragment = document.createDocumentFragment()
    fragment.appendChild(ageStatsAverageParagraph)
    fragment.appendChild(ageStatsEcartParagraph)
    
    ageStatsContainer.appendChild(fragment)
    
    return ageStatsContainer
}

function createStatisticServerValueRepresentation(ServersStats) {
    console.log(ServersStats)
    let divContainer = document.createElement("div")
    let ServersStatsValue = ServersStats.ServersStats.sort((a, b) => (a.Value > b.Value) ? -1 : ((b.Value > a.Value) ? 1 : 0))
    let serversStatsRecovered = document.createElement("table")
    let firstLine = document.createElement("tr")
    let firstColumn = document.createElement("th")
    let secondColumn = document.createElement("th")

    firstColumn.innerText = "Nom du serveur"
    secondColumn.innerText = "Total"

    firstLine.appendChild(firstColumn)
    firstLine.appendChild(secondColumn)
    serversStatsRecovered.appendChild(firstLine)

    for (let i = 0; i < ServersStatsValue.length; i++) {
        let line = document.createElement("tr")
        let firstColumn = document.createElement("td")
        let secondColumn = document.createElement("td")

        firstColumn.innerText = ServersStatsValue[i].Name
        secondColumn.innerText = ServersStatsValue[i].Value

        line.appendChild(firstColumn)
        line.appendChild(secondColumn)
        serversStatsRecovered.appendChild(line)
    }
    
    divContainer.appendChild(serversStatsRecovered)

    return divContainer
}
