import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { CalculateGeoDistanceRequest } from "../models/calculate-geo-distance-request";

@Injectable({
    providedIn: 'root',
})
export class GeoDistanceClientService
{

    constructor(
        private httpClient: HttpClient
    ) { }

    public calculate(request: CalculateGeoDistanceRequest): Observable<number> {

        return this.httpClient.post<number>('geo-distance/calculate', request);
    }
}