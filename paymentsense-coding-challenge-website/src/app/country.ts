import { logging } from "protractor";

export interface Country {
    name: string;

    alpha2Code: string;

    capital: string;

    population: bigint;

    numericCode: number;

    currencies: Currency[];

    languages: Language[];

    flag: string;

}

export interface Currency {
    code: string;

    name: string;

    symbol: string;
}

export interface Language {
    name: string;
}