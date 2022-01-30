import { DistanceUnit } from './distance-unit.enum';
import { GeoDistanceCalculationMethod } from './geo-distance-calculation-method.enum';

export class CalculateGeoDistanceRequest {

    locationALatitude: number;
    locationALongitude: number;
    locationBLatitude: number;
    locationBLongitude: number;
    unit: DistanceUnit;
    method: GeoDistanceCalculationMethod;
}
