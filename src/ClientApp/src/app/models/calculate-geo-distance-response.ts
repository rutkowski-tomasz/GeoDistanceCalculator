import { DistanceUnit } from './distance-unit.enum';
import { GeoDistanceCalculationMethod } from './geo-distance-calculation-method.enum';

export class CalculateGeoDistanceResponse {

    value: number;
    unit: DistanceUnit;
    method: GeoDistanceCalculationMethod;
}
