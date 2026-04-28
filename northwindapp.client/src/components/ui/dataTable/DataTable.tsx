import {
  ColumnDef,
  ColumnFiltersState,
  OnChangeFn,
  PaginationState,
  SortingState,
  flexRender,
  getCoreRowModel,
  getFilteredRowModel,
  getPaginationRowModel,
  getSortedRowModel,
  useReactTable,
} from '@tanstack/react-table';
import { ReactNode, useState } from 'react';

type DataTableProps<TData> = {
  columns: ColumnDef<TData, any>[];
  data: TData[];
  toolbar?: ReactNode;
  onRowClick?: (row: TData) => void;
  emptyMessage?: string;
  className?: string;
  isLoading?: boolean;
  sorting?: SortingState;
  onSortingChange?: OnChangeFn<SortingState>;
  pagination?: PaginationState;
  onPaginationChange?: OnChangeFn<PaginationState>;
  pageCount?: number;
};

export function DataTable<TData>({
  columns,
  data,
  toolbar,
  onRowClick,
  emptyMessage = 'No results.',
  className = '',
  isLoading = false,
  sorting: sortingProp,
  onSortingChange,
  pagination: paginationProp,
  onPaginationChange,
  pageCount,
}: DataTableProps<TData>) {
  const isServerSorted = sortingProp !== undefined;
  const isServerPaginated = paginationProp !== undefined;

  const [internalSorting, setInternalSorting] = useState<SortingState>([]);
  const [internalPagination, setInternalPagination] = useState<PaginationState>(
    { pageIndex: 0, pageSize: 25 },
  );
  const [columnFilters, setColumnFilters] = useState<ColumnFiltersState>([]);

  const table = useReactTable({
    data,
    columns,
    pageCount: isServerPaginated ? (pageCount ?? -1) : undefined,
    state: {
      sorting: isServerSorted ? sortingProp : internalSorting,
      pagination: isServerPaginated ? paginationProp : internalPagination,
      columnFilters,
    },
    onSortingChange: isServerSorted ? onSortingChange : setInternalSorting,
    onPaginationChange: isServerPaginated
      ? onPaginationChange
      : setInternalPagination,
    onColumnFiltersChange: setColumnFilters,
    manualSorting: isServerSorted,
    manualPagination: isServerPaginated,
    getCoreRowModel: getCoreRowModel(),
    getSortedRowModel: isServerSorted ? undefined : getSortedRowModel(),
    getPaginationRowModel: isServerPaginated
      ? undefined
      : getPaginationRowModel(),
    getFilteredRowModel: getFilteredRowModel(),
  });

  return (
    <div
      className={`rounded-lg border border-gray-200 bg-white shadow-sm ${className}`}
    >
      {toolbar && (
        <div className="flex flex-wrap items-center gap-3 border-b border-gray-200 px-4 py-3">
          {toolbar}
        </div>
      )}

      <div className="relative overflow-x-auto">
        <table className="w-full text-sm">
          <thead>
            {table.getHeaderGroups().map((headerGroup) => (
              <tr
                key={headerGroup.id}
                className="border-b border-gray-200 bg-gray-50/50"
              >
                {headerGroup.headers.map((header) => {
                  const canSort = header.column.getCanSort();
                  const sortDir = header.column.getIsSorted();
                  return (
                    <th
                      key={header.id}
                      className={`px-4 py-3 text-left text-xs font-semibold uppercase tracking-wide text-gray-600 ${
                        canSort ? 'cursor-pointer select-none' : ''
                      }`}
                      onClick={
                        canSort
                          ? header.column.getToggleSortingHandler()
                          : undefined
                      }
                    >
                      <span className="inline-flex items-center gap-1">
                        {header.isPlaceholder
                          ? null
                          : flexRender(
                              header.column.columnDef.header,
                              header.getContext(),
                            )}
                        {canSort && (
                          <span className="text-gray-400">
                            {sortDir === 'asc'
                              ? '↑'
                              : sortDir === 'desc'
                                ? '↓'
                                : '↕'}
                          </span>
                        )}
                      </span>
                    </th>
                  );
                })}
              </tr>
            ))}
          </thead>
          <tbody>
            {table.getRowModel().rows.length === 0 ? (
              <tr>
                <td
                  colSpan={columns.length}
                  className="px-4 py-10 text-center text-gray-500"
                >
                  {isLoading ? 'Loading…' : emptyMessage}
                </td>
              </tr>
            ) : (
              table.getRowModel().rows.map((row) => (
                <tr
                  key={row.id}
                  onClick={
                    onRowClick ? () => onRowClick(row.original) : undefined
                  }
                  className={`border-b border-gray-100 last:border-0 ${
                    onRowClick ? 'cursor-pointer hover:bg-gray-50' : ''
                  }`}
                >
                  {row.getVisibleCells().map((cell) => (
                    <td key={cell.id} className="px-4 py-3 text-gray-800">
                      {flexRender(
                        cell.column.columnDef.cell,
                        cell.getContext(),
                      )}
                    </td>
                  ))}
                </tr>
              ))
            )}
          </tbody>
        </table>

        {isLoading && data.length > 0 && (
          <div className="pointer-events-none absolute inset-0 bg-white/40" />
        )}
      </div>

      <div className="flex items-center justify-between border-t border-gray-200 px-4 py-3 text-sm text-gray-600">
        <span>
          Page {table.getState().pagination.pageIndex + 1}
          {table.getPageCount() > 0 && ` of ${table.getPageCount()}`}
        </span>
        <div className="flex gap-2">
          <button
            onClick={() => table.previousPage()}
            disabled={!table.getCanPreviousPage()}
            className="rounded-md border border-gray-300 px-3 py-1 disabled:opacity-50"
          >
            Previous
          </button>
          <button
            onClick={() => table.nextPage()}
            disabled={!table.getCanNextPage()}
            className="rounded-md border border-gray-300 px-3 py-1 disabled:opacity-50"
          >
            Next
          </button>
        </div>
      </div>
    </div>
  );
}
