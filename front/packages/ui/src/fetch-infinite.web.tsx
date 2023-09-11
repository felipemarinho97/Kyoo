/*
 * Kyoo - A portable and vast media library solution.
 * Copyright (c) Kyoo.
 *
 * See AUTHORS.md and LICENSE file in the project root for full license information.
 *
 * Kyoo is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * any later version.
 *
 * Kyoo is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with Kyoo. If not, see <https://www.gnu.org/licenses/>.
 */

import { Page, QueryIdentifier, useInfiniteFetch } from "@kyoo/models";
import { HR } from "@kyoo/primitives";
import { ComponentType, Fragment, isValidElement, ReactElement, useEffect, useRef } from "react";
import { Stylable, useYoshiki } from "yoshiki";
import { EmptyView, ErrorView, Layout, WithLoading } from "./fetch";

const InfiniteScroll = <Props,>({
	children,
	loader,
	layout = "vertical",
	loadMore,
	hasMore = true,
	isFetching,
	Header,
	headerProps,
	...props
}: {
	children?: ReactElement | (ReactElement | null)[] | null;
	loader?: (ReactElement | null)[];
	layout?: "vertical" | "horizontal" | "grid";
	loadMore: () => void;
	hasMore: boolean;
	isFetching: boolean;
	Header?: ComponentType<Props & { children: JSX.Element }> | ReactElement;
	headerProps?: Props
} & Stylable) => {
	const ref = useRef<HTMLDivElement>(null);
	const { css } = useYoshiki();

	// Automatically trigger a scroll check on start and after a fetch end in case the user is already
	// at the bottom of the page or if there is no scroll bar (ultrawide or something like that)
	useEffect(() => {
		onScroll();
	}, [isFetching]);

	const onScroll = () => {
		if (!ref.current || !hasMore || isFetching) return;
		const scroll =
			layout === "horizontal"
				? ref.current.scrollWidth - ref.current.scrollLeft
				: ref.current.scrollHeight - ref.current.scrollTop;
		const offset = layout === "horizontal" ? ref.current.offsetWidth : ref.current.offsetHeight;

		if (scroll <= offset * 1.2) loadMore();
	};
	const scrollProps = { ref, onScroll };

	const list = (props: object) => (
		<div
			{...css(
				[
					{
						display: "flex",
						alignItems: "flex-start",
						overflowX: "hidden",
						overflowY: "auto",
					},
					layout == "vertical" && {
						flexDirection: "column",
						alignItems: "stretch",
					},
					layout == "horizontal" && {
						flexDirection: "row",
						alignItems: "stretch",
					},
					layout === "grid" && {
						flexWrap: "wrap",
						justifyContent: "center",
					},
				],

				props,
			)}
		>
			{children}
			{hasMore && isFetching && loader}
		</div>
	);

	if (!Header) return list({ ...scrollProps, ...props });
	// @ts-ignore
	if (!isValidElement(Header)) return <Header {...scrollProps} {...headerProps}>{list(props)}</Header>;
	return (
		<>
			{Header}
			{list({ ...scrollProps, ...props })}
		</>
	);
};

export const InfiniteFetch = <Data, _>({
	query,
	incremental = false,
	placeholderCount = 15,
	children,
	layout,
	horizontal = false,
	empty,
	divider: Divider = false,
	Header,
	getItemType,
	...props
}: {
	query: QueryIdentifier<_, Data>;
	incremental?: boolean;
	placeholderCount?: number;
	layout: Layout;
	horizontal?: boolean;
	children: (
		item: Data extends Page<infer Item> ? WithLoading<Item> : WithLoading<Data>,
		i: number,
	) => ReactElement | null;
	empty?: string | JSX.Element;
	divider?: boolean | ComponentType;
	Header?: ComponentType<{ children: JSX.Element }> | ReactElement;
	getItemType?: (item: Data, index: number) => string | number;
}): JSX.Element | null => {
	if (!query.infinite) console.warn("A non infinite query was passed to an InfiniteFetch.");

	const oldItems = useRef<Data[] | undefined>();
	const { items, error, fetchNextPage, hasNextPage, isFetching } = useInfiniteFetch(query, {
		useErrorBoundary: false,
	});
	const grid = layout.numColumns !== 1;

	if (incremental && items) oldItems.current = items;

	if (error) return addHeader(Header, <ErrorView error={error} />);
	if (empty && items && items.length === 0) {
		if (typeof empty !== "string") return empty;
		return <EmptyView message={empty} />;
	}

	return (
		<InfiniteScroll
			layout={grid ? "grid" : horizontal ? "horizontal" : "vertical"}
			loadMore={fetchNextPage}
			hasMore={hasNextPage!}
			isFetching={isFetching}
			loader={[...Array(12)].map((_, i) => (
				<Fragment key={i.toString()}>
					{Divider && i !== 0 && (Divider === true ? <HR /> : <Divider />)}
					{children({ isLoading: true } as any, i)}
				</Fragment>
			))}
			Header={Header}
			{...props}
		>
			{(items ?? oldItems.current)?.map((item, i) => (
				<Fragment key={(item as any).id?.toString()}>
					{Divider && i !== 0 && (Divider === true ? <HR /> : <Divider />)}
					{children({ ...item, isLoading: false } as any, i)}
				</Fragment>
			))}
		</InfiniteScroll>
	);
};

const addHeader = (
	Header: ComponentType<{ children: JSX.Element }> | ReactElement | undefined,
	children: ReactElement,
) => {
	if (!Header) return children;
	return typeof Header === "function" ? (
		<Header>{children}</Header>
	) : (
		<>
			{Header}
			{children}
		</>
	);
};
