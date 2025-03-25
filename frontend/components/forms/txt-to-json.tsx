"use client";
import { Input } from "@/components/ui/input";
import { SocialType } from "@/types/general";
import Papa from "papaparse";

interface CsvToJsonProps {
    parsedJsonData: (data: SocialType[]) => void;
}

const TxtToJson: React.FC<CsvToJsonProps> = ({ parsedJsonData }) => {
    const handleFileUpload = (event: React.ChangeEvent<HTMLInputElement>) => {
        const file = event.target.files?.[0];

        if (!file) return;

        Papa.parse(file, {
            header: false, // Parses the first row as object keys
            delimiter: " ",
            skipEmptyLines: true,
            newline: "\n",
            complete: (result) => {
                const res = result.data.map((item: any) => ({
                    userId: Number(item[0]),
                    friendId: Number(item[1]),
                }));
                console.log("Parsed JSON:", res);
                console.log("lenght: ", res.length);
                parsedJsonData(res);
            },
            error: (error) => {
                console.error("Error parsing CSV:", error);
            },
        });
    };

    return (
        <div>
            <Input id="picture" type="file" accept=".txt" onChange={handleFileUpload} />
        </div>
    );
};

export default TxtToJson;
