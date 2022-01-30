import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { CalculateGeoDistanceRequest } from "../models/calculate-geo-distance-request";
import { CalculateGeoDistanceResponse } from "../models/calculate-geo-distance-response";

@Injectable({
    providedIn: 'root',
})
export class GeoDistanceClientService
{

    constructor(
        private httpClient: HttpClient
    ) { }

    public calculate(request: CalculateGeoDistanceRequest): Observable<CalculateGeoDistanceResponse> {

        return this.httpClient.post<CalculateGeoDistanceResponse>('geo-distance/calculate', request);
    }
}