import {Injectable} from '@angular/core';
import {City, Country, ICity, ICountry} from "country-state-city";

@Injectable({
  providedIn: 'root'
})
export class LocationService {

  constructor() {
  }

  getCountries(): ICountry[] {
    return Country.getAllCountries();
  }

  getCities(countryCode: string): ICity[] | undefined {
    return City.getCitiesOfCountry(countryCode);
  }

}
