import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { catchError, debounceTime, filter, map, startWith, switchMap } from 'rxjs/operators';
import { HttpRequestState } from '../common/http-request-state';
import {
    CalculateGeoDistanceEndpointClient,
    CalculateGeoDistanceRequest,
    CalculateGeoDistanceResponse,
    DistanceUnit,
    GeoDistanceCalculationMethod
} from '../services/web-api-client';

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
        private calculateGeoDistanceClient: CalculateGeoDistanceEndpointClient
    ) { }

    public ngOnInit(): void {

        this.geoDistanceForm = new FormGroup({
            initialLatitude: new FormControl(53.297975, [Validators.required, Validators.pattern(/^\-?\d+(\.\d+)?$/)]),
            initialLongitude: new FormControl(-6.372663, [Validators.required, Validators.pattern(/^\-?\d+(\.\d+)?$/)]),
            targetLatitude: new FormControl(41.385101, [Validators.required, Validators.pattern(/^\-?\d+(\.\d+)?$/)]),
            targetLongitude: new FormControl(-81.440440, [Validators.required, Validators.pattern(/^\-?\d+(\.\d+)?$/)]),
            unit: new FormControl(DistanceUnit[DistanceUnit.Kilometer], [Validators.required]),
            method: new FormControl(GeoDistanceCalculationMethod[GeoDistanceCalculationMethod.GeoCurve], [Validators.required]),
        });

        this.distance$ = this.geoDistanceForm.valueChanges.pipe(
            startWith(this.geoDistanceForm.value),
            debounceTime(250),
            filter(_ => this.geoDistanceForm.valid),
            map(formValues => this.buildCalculateGeoDistanceRequest(formValues)),
            switchMap(query => this.calculateGeoDistanceClient.handle(query).pipe(
                map((value) => ({isLoading: false, value})),
                catchError(error => of({isLoading: false, error})),
                startWith({isLoading: true})
            ))
        );
    }

    private buildCalculateGeoDistanceRequest(formValues: any): CalculateGeoDistanceRequest {

        const request = new CalculateGeoDistanceRequest(formValues);
        return request;
    }
}
