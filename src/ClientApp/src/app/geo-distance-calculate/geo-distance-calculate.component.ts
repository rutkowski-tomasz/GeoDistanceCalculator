import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { catchError, debounceTime, filter, map, startWith, switchMap } from 'rxjs/operators';
import { HttpRequestState } from '../common/http-request-state';
import { CalculateGeoDistanceRequest, CalculateGeoDistanceResponse, Client, DistanceUnit, GeoDistanceCalculationMethod } from '../services/web-api-client';

@Component({
    selector: 'app-geo-distance-calculate',
    templateUrl: './geo-distance-calculate.component.html',
    styleUrls: ['./geo-distance-calculate.component.scss']
})
export class GeoDistanceCalculateComponent implements OnInit {

    public DistanceUnit = DistanceUnit;
    public GeoDistanceCalculationMethod = GeoDistanceCalculationMethod;

    public geoDistanceForm: FormGroup;
    public distance$: Observable<HttpRequestState<CalculateGeoDistanceResponse>>;
    public isLoading$ = new BehaviorSubject<boolean>(false);

    constructor(
        private client: Client
    ) { }

    public ngOnInit(): void {

        console.log(Object.keys(GeoDistanceCalculationMethod));

        this.geoDistanceForm = new FormGroup({
            locationALatitude: new FormControl(53.297975, [Validators.required, Validators.pattern(/^\-?\d+(\.\d+)?$/)]),
            locationALongitude: new FormControl(-6.372663, [Validators.required, Validators.pattern(/^\-?\d+(\.\d+)?$/)]),
            locationBLatitude: new FormControl(41.385101, [Validators.required, Validators.pattern(/^\-?\d+(\.\d+)?$/)]),
            locationBLongitude: new FormControl(-81.440440, [Validators.required, Validators.pattern(/^\-?\d+(\.\d+)?$/)]),
            unit: new FormControl(DistanceUnit[DistanceUnit.Kilometer], [Validators.required]),
            method: new FormControl(GeoDistanceCalculationMethod[GeoDistanceCalculationMethod.GeoCurve], [Validators.required]),
        });

        this.distance$ = this.geoDistanceForm.valueChanges.pipe(
            startWith(this.geoDistanceForm.value),
            debounceTime(250),
            filter(_ => this.geoDistanceForm.valid),
            map(formValues => this.buildCalculateGeoDistanceRequest(formValues)),
            switchMap(query => this.client.geoDistance_Calculate(query).pipe(
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
        request.method = formValues.method;

        return request;
    }
}
