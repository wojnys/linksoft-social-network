import { ColumnDef } from "@tanstack/react-table";
import { analysisTableDataType } from "@/types/general";
import { Button } from "@/components/ui/button";
import { ArrowUpDown } from "lucide-react";

export const columns: ColumnDef<analysisTableDataType>[] = [
    {
        accessorKey: "id",
        header: "Dataset ID",
        cell: ({ row }) => <div className="capitalize">{row.getValue("id")}</div>,
    },
    {
        accessorKey: "name",
        header: "Dataset name",
        cell: ({ row }) => <div className="capitalize">{row.getValue("name")}</div>,
    },
    {
        accessorKey: "averageFriendsPerUser",
        header: "Average Friends Per User",
        cell: ({ row }) => <div className="capitalize">{row.getValue("averageFriendsPerUser")}</div>,
    },
    {
        accessorKey: "usersCount",
        header: ({ column }) => (
            <Button variant="ghost" onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}>
                Users count overall
                <ArrowUpDown />
            </Button>
        ),
    },
];
