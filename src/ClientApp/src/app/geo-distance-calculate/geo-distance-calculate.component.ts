import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { BehaviorSubject, Observable, of, Subscription } from 'rxjs';
import { catchError, debounceTime, finalize, map, startWith, switchMap, tap } from 'rxjs/operators';
import { HttpRequestState } from '../common/http-request-state';
import { CalculateGeoDistanceRequest } from '../models/calculate-geo-distance-request';
import { GeoDistanceClientService } from '../services/geo-distance-client.service';

@Component({
    selector: 'app-geo-distance-calculate',
    templateUrl: './geo-distance-calculate.component.html',
    styleUrls: ['./geo-distance-calculate.component.scss']
})
export class GeoDistanceCalculateComponent implements OnInit {

    public geoDistanceForm: FormGroup;
    public distance$: Observable<HttpRequestState<number>>;
    public isLoading$ = new BehaviorSubject<boolean>(false);

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
            // tap(_ => console.log('asdaa')),
            debounceTime(250),
            // tap(_ => console.log('asd')),
            map(formValues => this.buildCalculateGeoDistanceRequest(formValues)),
            // tap(_ => this.isLoading$.next(true)),
            // switchMap(query => this.client.calculate(query).pipe(
            //     tap(_ => console.log('test')),
            //     tap(() => this.isLoading$.next(false))
            // )),

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

        return request;
    }
}
