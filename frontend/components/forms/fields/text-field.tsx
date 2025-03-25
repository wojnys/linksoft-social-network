import { Input } from "@/components/ui/input";
import { Label } from "@radix-ui/react-label";

const TextField = ({ field }: any) => (
    <div className="space-y-2">
        <Label htmlFor="name">Dataset Name</Label>
        <Input id="dataset" placeholder="Enter dataset name" onChange={(e) => field.handleChange(e.target.value)} />
        {field.state.meta.errors.length ? (
            <em role="alert" className="text-red-500">
                {field.state.meta.errors.join(", ")}
            </em>
        ) : null}
    </div>
);

export default TextField;
