import { DistanceUnit } from './distance-unit.enum';

export class CalculateGeoDistanceRequest {

    locationALatitude: number;
    locationALongitude: number;
    locationBLatitude: number;
    locationBLongitude: number;
    unit: DistanceUnit;
}
