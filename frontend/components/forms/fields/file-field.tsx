import TxtToJson from "@/components/forms/txt-to-json";
import { Label } from "@radix-ui/react-label";

const FileField = ({ field }: any) => (
    <div className="space-y-2">
        <Label>File</Label>
        <TxtToJson parsedJsonData={(data) => field.handleChange(data)} />
        {field.state.meta.errors.length ? (
            <em role="alert" className="text-red-500">
                {field.state.meta.errors.join(", ")}
            </em>
        ) : null}
    </div>
);

export default FileField;
