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

import { ImageProps, ImageStyle, Platform, View, ViewProps, ViewStyle } from "react-native";
import { Props, YoshikiEnhanced } from "./base-image";
import { Image } from "./image";
import { ComponentType, ReactNode, useState } from "react";
import { LinearGradient, LinearGradientProps } from "expo-linear-gradient";
import { ContrastArea, alpha } from "../themes";
import { percent } from "yoshiki/native";

export { type Props as ImageProps, Image };

export const Poster = ({
	alt,
	layout,
	...props
}: Props & { style?: ImageStyle } & {
	layout: YoshikiEnhanced<{ width: ImageStyle["width"] } | { height: ImageStyle["height"] }>;
}) => <Image alt={alt!} layout={{ aspectRatio: 2 / 3, ...layout }} {...props} />;

export const ImageBackground = <AsProps = ViewProps,>({
	src,
	alt,
	quality,
	gradient = true,
	as,
	children,
	containerStyle,
	imageStyle,
	forcedLoading,
	...asProps
}: {
	as?: ComponentType<AsProps>;
	gradient?: Partial<LinearGradientProps> | boolean;
	children: ReactNode;
	containerStyle?: YoshikiEnhanced<ViewStyle>;
	imageStyle?: YoshikiEnhanced<ImageStyle>;
} & AsProps &
	Props) => {
	const Container = as ?? View;
	return (
		<ContrastArea contrastText>
			{({ css, theme }) => (
				<Container {...(asProps as AsProps)}>
					<View
						{...css([
							{
								position: "absolute",
								top: 0,
								bottom: 0,
								left: 0,
								right: 0,
								zIndex: -1,
								bg: (theme) => theme.background,
							},
							containerStyle,
						])}
					>
						{src && (
							<Image
								src={src}
								quality={quality}
								forcedLoading={forcedLoading}
								alt={alt!}
								layout={{ width: percent(100), height: percent(100) }}
								Error={null}
								{...(css([{ borderWidth: 0, borderRadius: 0 }, imageStyle]) as {
									style: ImageStyle;
								})}
							/>
						)}
						{gradient && (
							<LinearGradient
								start={{ x: 0, y: 0.25 }}
								end={{ x: 0, y: 1 }}
								colors={["transparent", alpha(theme.colors.black, 0.6)]}
								{...css(
									{
										position: "absolute",
										top: 0,
										bottom: 0,
										left: 0,
										right: 0,
									},
									typeof gradient === "object" ? gradient : undefined,
								)}
							/>
						)}
					</View>
					{children}
				</Container>
			)}
		</ContrastArea>
	);
};
