import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { catchError, debounceTime, filter, map, startWith, switchMap } from 'rxjs/operators';
import { HttpRequestState } from '../common/http-request-state';
import { CalculateGeoDistanceRequest } from '../models/calculate-geo-distance-request';
import { CalculateGeoDistanceResponse } from '../models/calculate-geo-distance-response';
import { DistanceUnit } from '../models/distance-unit.enum';
import { GeoDistanceClientService } from '../services/geo-distance-client.service';

@Component({
    selector: 'app-geo-distance-calculate',
    templateUrl: './geo-distance-calculate.component.html',
    styleUrls: ['./geo-distance-calculate.component.scss']
})
export class GeoDistanceCalculateComponent implements OnInit {

    public geoDistanceForm: FormGroup;
    public distance$: Observable<HttpRequestState<CalculateGeoDistanceResponse>>;
    public isLoading$ = new BehaviorSubject<boolean>(false);
    public DistanceUnit = DistanceUnit;

    constructor(
        private client: GeoDistanceClientService
    ) { }

    public ngOnInit(): void {

        this.geoDistanceForm = new FormGroup({
            locationALatitude: new FormControl(53.297975, [Validators.required, Validators.pattern(/^\-?\d+(\.\d+)?$/)]),
            locationALongitude: new FormControl(-6.372663, [Validators.required, Validators.pattern(/^\-?\d+(\.\d+)?$/)]),
            locationBLatitude: new FormControl(41.385101, [Validators.required, Validators.pattern(/^\-?\d+(\.\d+)?$/)]),
            locationBLongitude: new FormControl(-81.440440, [Validators.required, Validators.pattern(/^\-?\d+(\.\d+)?$/)]),
            unit: new FormControl(DistanceUnit[DistanceUnit.Kilometer], [Validators.required])
        });

        this.distance$ = this.geoDistanceForm.valueChanges.pipe(
            startWith(this.geoDistanceForm.value),
            debounceTime(250),
            filter(_ => this.geoDistanceForm.valid),
            map(formValues => this.buildCalculateGeoDistanceRequest(formValues)),
            switchMap(query => this.client.calculate(query).pipe(
                map((value) => ({isLoading: false, value})),
                catchError(error => of({isLoading: false, error})),
                startWith({isLoading: true})
            ))
        );
    }

    private buildCalculateGeoDistanceRequest(formValues: any): CalculateGeoDistanceRequest {

        const request = new CalculateGeoDistanceRequest();

        request.locationALatitude = parseFloat(formValues.locationALatitude);
        request.locationALongitude = parseFloat(formValues.locationALongitude);
        request.locationBLatitude = parseFloat(formValues.locationBLatitude);
        request.locationBLongitude = parseFloat(formValues.locationBLongitude);
        request.unit = formValues.unit;

        return request;
    }
}
