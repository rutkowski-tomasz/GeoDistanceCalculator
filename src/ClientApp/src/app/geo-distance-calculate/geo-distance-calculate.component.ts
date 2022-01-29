import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Observable, Subscription } from 'rxjs';
import { debounceTime, map, switchMap, filter } from 'rxjs/operators';
import { CalculateGeoDistanceRequest } from '../models/calculate-geo-distance-request';
import { GeoDistanceClientService } from '../services/geo-distance-client.service';

@Component({
    selector: 'app-geo-distance-calculate',
    templateUrl: './geo-distance-calculate.component.html'
})
export class GeoDistanceCalculateComponent implements OnInit {

    geoDistanceForm: FormGroup;
    distance$: Observable<number>;

    constructor(
        private client: GeoDistanceClientService
    ) { }

    public ngOnInit(): void {

        this.geoDistanceForm = new FormGroup({
            locationALatitude: new FormControl(53.297975),
            locationALongitude: new FormControl(-6.372663),
            locationBLatitude: new FormControl(41.385101),
            locationBLongitude: new FormControl(-81.440440),
        });

        this.distance$ = this.geoDistanceForm.valueChanges.pipe(
            debounceTime(250),
            map(formValues => this.buildCalculateGeoDistanceRequest(formValues)),
            switchMap(query => this.client.calculate(query))
        );
    }

    private buildCalculateGeoDistanceRequest(formValues: any): CalculateGeoDistanceRequest {

        const request = new CalculateGeoDistanceRequest();

        request.locationALatitude = parseFloat(formValues.locationALatitude);
        request.locationALongitude = parseFloat(formValues.locationALongitude);
        request.locationBLatitude = parseFloat(formValues.locationBLatitude);
        request.locationBLongitude = parseFloat(formValues.locationBLongitude);

        return request;
    }
}
