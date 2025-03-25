import { Button } from "@/components/ui/button";

interface TablePaginationProps {
    canPreviousPage: boolean;
    canNextPage: boolean;

    onPreviousPage: () => void;
    onNextPage: () => void;
}

const TablePagination: React.FC<TablePaginationProps> = ({ canPreviousPage, canNextPage, onPreviousPage, onNextPage }) => {
    return (
        <div className="flex items-center justify-center space-x-2 py-4">
            <div className="space-x-2">
                <Button variant="outline" size="sm" onClick={onPreviousPage} disabled={!canPreviousPage}>
                    Previous
                </Button>

                <Button variant="outline" size="sm" onClick={onNextPage} disabled={!canNextPage}>
                    Next
                </Button>
            </div>
        </div>
    );
};

export default TablePagination;
