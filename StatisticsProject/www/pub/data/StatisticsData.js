class StatsData {
    constructor(urls) {
        this.urls = urls;
        this.ServersStats = []; // Need to change
        this.nbCookiesSetAverage = 0;
        this.nbSecondsFromModificationAverage = 0;
        this.IsAltSvcAverage = 0;
    }
}

class ServerContainerValue {
    constructor(Name, Value) {
        this.Name = Name;
        this.Value = Value;
    }
}
